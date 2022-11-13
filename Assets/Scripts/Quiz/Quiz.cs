using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents a game service to run the quiz. Makes extensive use of the dialogue service in order to provide this functioanlity.
/// Quiz questions are deserialized from a JSON file (but never serialized).
/// </summary>
public class Quiz
{
    private static readonly int QUESTIONS_PER_QUIZ = 5;

    /// <summary>
    /// Represents an answer to the quiz, either correct or incorrect.
    /// </summary>
    [Serializable]
    public class Answer
    {
        public string value;
        public bool correct;
    }
    
    /// <summary>
    /// Represents a quiz question with a set of answers.
    /// </summary>
    [Serializable]
    public class Question
    {
        public string question;
        public string explanation;
        public List<Answer> answers;

        /// <summary>
        /// Make a small dialogue tree out of this question.
        /// </summary>
        /// <param name="onAnswerSelected">A callback that is executed when the player selects an answer.
        /// The boolean argument indicates whether the </param>
        /// <returns>The root of the tree that was created</returns>
        public DialogueElement ToDialogueElement(Action<bool> onAnswerSelected)
        {
            return new DialogueElement(null, question, null,
                answers.Select(a => new DialogueTerminal(a.value, () => onAnswerSelected(a.correct))));
        }

        public Answer CorrectAnswer => answers.Single(a => a.correct);
    }

    /// <summary>
    /// Unfortunately, this wrapper is required by Unity's JSON reader. It cannot directly deserialize lists and so we need
    /// a wrapper object for our list of Question objects. We format the quiz question pool JSON to follow this format.
    /// </summary>
    [Serializable]
    private class QuestionCollection // This wrapper is required by Unity's JSON reader - cannot directly deserialize a list.
    {
        public List<Question> questions = null;
    }

    private List<Question> questions;
    private System.Random random = new System.Random();

    /// <summary>
    /// Initialize the quiz object. The question pool is initialized from a JSON file in Resources.
    /// </summary>
    /// <param name="questionsPath">The path of the questions resource to load</param>
    public Quiz(string questionsPath)
    {
        string json = UnityUtil.ReadTextResource(questionsPath);
        questions = JsonUtility.FromJson<QuestionCollection>(json).questions;
        if (questions.Count < QUESTIONS_PER_QUIZ)
        { throw new ArgumentException("Too few questions! Questions per quiz > number of questions in this resource."); }
    }

    /// <summary>
    /// Run the quiz. Picks a sample of questions from the question pool and constructs the appropriate dialogue.
    /// Score is updated according to the player's final score and the payer is given a feedback message on how
    /// well they did.
    /// </summary>
    public void Run()
    {
        DialogueManager dialogueManager = SharedCanvas.Instance.dialogueManager;

        int questionIndex = 0;
        int correctCount = 0;

        // First pick a random sample of questions.
        List<Question> sample = questions.OrderBy(q => random.Next()).Take(QUESTIONS_PER_QUIZ).ToList();
        
        // Define local helper methods to generate the next dialogue nodes for the quiz and run them.
        void Next()
        {
            if (questionIndex < sample.Count)
            {
                // Get the next question.
                Question question = sample[questionIndex];
                questionIndex++;

                // Generate a dialogue node for the question.
                DialogueElement questionNode = new DialogueElement(null, question.question, null,
                    question.answers.Select(a => new DialogueTerminal(a.value, () => RespondToAnswer(question, a))));

                // Run the node.
                dialogueManager.RunDialogue(questionNode);
            }
            else
            {
                // End the quiz.
                DialogueElement endNode = new DialogueElement(null,
                    $"You've reached the end of the quiz. You scored {correctCount} points out of {sample.Count}. Well done!", null,
                    new DialogueTerminal("OK"));

                ScoreManager scoreManager = SharedCanvas.Instance.scoreManager;
                float scoreChange = 4 * correctCount;
                scoreManager.ChangeCommunityScore(scoreChange);
                scoreManager.ChangePersonalScore(scoreChange);

                dialogueManager.RunDialogue(endNode);
            }
        }

        void RespondToAnswer(Question question, Answer answer)
        {
            // Give feedback for the answer.
            if (answer.correct) { correctCount++; }
            DialogueElement response = new DialogueElement(null, answer.correct ? "Yes! That's right." : $"Incorrect. {question.explanation}", null,
                new DialogueTerminal("OK", Next));
            dialogueManager.RunDialogue(response);
        }

        // Run the quiz!
        Next();
    }
}

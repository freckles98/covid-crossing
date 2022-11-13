using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Allows player to interact with the Computer and play the quiz.
/// </summary>
public class ComputerInteraction : Interaction
{
    private Quiz quiz;
    /// <summary>
    /// Starts the quiz, by calling on the quiz class.
    /// </summary>
    public void Start()
    {
        quiz = new Quiz("QuizQuestions");
    }
    /// <summary>
    /// Generates dialogue for the players to play the quiz. It also checks whether the quiz has been played already on the current day.
    /// </summary>
    protected override void Interact()
    {
        DialogueElement node = new DialogueElement(null, "Do you want to do a quiz on COVID-19?", null,
            new DialogueTerminal("Yes", () => { SetWasUsedToday(); quiz.Run(); }),
            new DialogueTerminal("No"));

        SharedCanvas.Instance.dialogueManager.RunDialogue(node);
    }

    protected override string GetName() => "Computer";
}

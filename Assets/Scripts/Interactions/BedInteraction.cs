using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  BedInteration inherits from the Interaction class and allows for the player to interact with the bed, which allows the player to go to bed and reset the day.
/// </summary>
public class BedInteraction : Interaction
{
    /// <summary>
    /// An overriden class from Interaction that creates dialogue when the player interacts with the bed, this causes a dialogue to pop up asking the player if they want to go to bed and reset the day.
    /// </summary> 
    protected override void Interact()
    {
        ScoreManager scoreManager = SharedCanvas.Instance.scoreManager;
       
        DialogueElement dialogue = new DialogueElement(null, "Do you want to go to bed?", null,
            new DialogueElement("Yes", "Are you sure? This will reset the day.", null,
                new DialogueTerminal("Yes", ResetDay),
                new DialogueTerminal("No")),
            new DialogueTerminal("No"));
        SharedCanvas.Instance.dialogueManager.RunDialogue(dialogue);

        //Resets the day by giving an overview of the past day scores and resets the score
        void ResetDay()
        {
            SharedCanvas.Instance.dayTracker.NextDay();

            DialogueElement dialogueReset = new DialogueElement(null, $"Here's an overlook of the day:\n" +
                $"Your previous high score: {scoreManager.GetHighScore()}\nYour community score: {scoreManager.GetCommunityScore()}\n" +
                $"Your personal score: {scoreManager.GetPersonalScore()}\nYour final score: {scoreManager.FinalScore()}\n" +
                $"You can check out the leaderboard by interacting with the TV!\n" +
                $"Enjoy the new day. ", null, new DialogueTerminal("OK"));
            SharedCanvas.Instance.dialogueManager.RunDialogue(dialogueReset);

            scoreManager.EndDayAndResetScores();
        }
    }
}
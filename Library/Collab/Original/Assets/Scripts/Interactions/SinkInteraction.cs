using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
/// <summary>
/// SinkInteration allows for the player to interact with the sink, which generates a dialogue that allows the player to wash their hands.
/// </summary>

public class SinkInteraction : Interaction
{
    /// <summary>
    /// Dialogue is generated that asks the player to wash their hands with or without soap and for a certain amount of time.
    /// </summary>
    protected override void Interact()
    {
        ScoreManager scoreManager = SharedCanvas.Instance.scoreManager;

        DialogueNode[] next = new DialogueNode[]
        {
            new DialogueElement("20 seconds", "Perfect! 20 seconds is recommended time for cleaning your hands.", () => scoreManager.ChangeScores(10, 10),
                new DialogueTerminal("OK")),
            new DialogueElement("2 minutes", "Nice job, you're hands are very clean, but you don't need to wash them for so long.", () => scoreManager.ChangeScores(5, 10),
                new DialogueTerminal("OK")),
            new DialogueElement("2 hours", "Wow! You spent two hours washing your hands! Your fingers are very wrinkly and you're feeling very tired.", () => scoreManager.ChangeScores(-20, 10),
                new DialogueTerminal("OK")),
            new DialogueElement("10 seconds", "Oh dear! 10 seconds is not long enough, you're hands are still dirty", () => scoreManager.ChangeScores(-10, -10),
                new DialogueTerminal("OK"))
        };

        DialogueElement dialogue = new DialogueElement(null, "Hey there! Do you want to wash your hands?", null,
            new DialogueElement("Yes", "Would you like to use soap?", SetWasUsedToday,
                new DialogueElement("Yes", "Responsible choice! Soap helps destroying bacteria and viruses. How long would you like to to wash hands for?", () => scoreManager.ChangeScores(10, 10), next),

                new DialogueElement("No", "Oh dear, by not using soap you might not kill off all the germs. How long would you like to to wash hands for?", () => scoreManager.ChangeScores(-10, -10), next)),
            new DialogueTerminal("No", null));
        SharedCanvas.Instance.dialogueManager.RunDialogue(dialogue);
        
    }

    protected override string Name() => "Sink";
}
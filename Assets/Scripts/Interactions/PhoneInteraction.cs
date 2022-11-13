using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Allows player to interact with the phone
/// </summary>
public class PhoneInteraction : Interaction
{
    /// <summary>
    /// Generates a dialogue that allows the player to make various decisions about who they want to call and consequestial questions that result from those decisions. These will affect the players personal and enviromental score.
    /// </summary>
    protected override void Interact()
    {

        ScoreManager scoreManager = SharedCanvas.Instance.scoreManager;

        DialogueElement dialogue = new DialogueElement(null, "Do you want to phone someone?", null, 
            new DialogueElement("Yes", "Who do you want to phone?", SetWasUsedToday,
                new DialogueElement("Granny", "She is happy to hear from you. She has been social distancing, as she is of high risk and is feeling a bit lonely. What do you want to say?", () => scoreManager.ChangeScores(5, 5),
                    new DialogueElement("Cheer her up", "She is grateful for your kind words and sends you a lucky pair of socks.", () => scoreManager.ChangeScores(5, 10),
                        new DialogueTerminal("Hang up the phone")),

                    new DialogueElement("Talk about yourself", "She pleased to hear about you, but is still feeling a bit sad.", () => scoreManager.ChangeScores(3, -5),
                        new DialogueTerminal("End the call"))),

                new DialogueElement("Your friend", "They say social distancing is not cool and they want to come over. What should you say?", null,
                    new DialogueElement("That's not a good idea.", "Well done it is very responsible to social distance.", () => scoreManager.ChangeScores(5, 10),
                        new DialogueTerminal("Hang up the phone")),
                    new DialogueElement("You invite them over", "That is not a good idea. Are you sure you want them to come over?", null,
                        new DialogueElement("Yes", "They try to go out and their parents get very angry. Your friend is in big trouble and so are you.", () => scoreManager.ChangeScores(-10, -10),
                            new DialogueTerminal("End the call quickly")),
                        new DialogueElement("No", "Good decision, don't give into peer pressure. Do what is right and social distance, this benefits you and your community.", () => scoreManager.ChangeScores(5, 10),
                            new DialogueTerminal("Hang up the phone"))))),
            new DialogueTerminal("No"));
        SharedCanvas.Instance.dialogueManager.RunDialogue(dialogue);
        
    }

    protected override string GetName() => "Phone";
    
}

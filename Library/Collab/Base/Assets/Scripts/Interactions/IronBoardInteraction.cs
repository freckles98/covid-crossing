using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Allows player to interact with the ironing board and iron their mask
/// </summary>
public class IronBoardInteraction : Interaction
{
    /// <summary>
    /// A dialogue will be generated which allows players to iron their masks which affects their personal and enviromental score
    /// </summary>
    protected override void Interact()
    {
        ScoreManager scoreManager = SharedCanvas.Instance.scoreManager;

        DialogueElement dialogue = new DialogueElement(null, "Do you want to iron your mask?", null,
            new DialogueElement("Yes", "You iron your mask. How satisfying.", () => scoreManager.ChangeScores(-15, 25),
                new DialogueTerminal("OK")),
            new DialogueTerminal("No"));

        SharedCanvas.Instance.dialogueManager.RunDialogue(dialogue);
    }

    protected override string GetName() => "IronBoard";

}

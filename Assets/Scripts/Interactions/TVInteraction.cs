using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 /// <summary>
 /// TVInteration inherits from the Interaction class and allows for the player to interact with the TV, thus displaying the Leaderboard.
 /// </summary>
public class TVInteraction : Interaction
{
    /// <summary>
    /// An overriden class from Interaction that creates dialogue when the player interats with the TV, that generates the Leaderboard.
    /// </summary>
    protected override void Interact()
    {
        DialogueElement node = new DialogueElement(null, "Do you want to view your place on the online leaderboard?", null,
            new DialogueTerminal("Yes", () => SharedCanvas.Instance.leaderboardManager.ShowLeaderboard()),
            new DialogueTerminal("No"));
        SharedCanvas.Instance.dialogueManager.RunDialogue(node);
    }
}

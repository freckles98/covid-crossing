using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A small easter egg class, that allows the players to interact with the book and see who created the COVID Crossing game.
/// </summary>
public class BookInteraction : Interaction
{
    /// <summary>
    /// Generates dialogue to show the names of the game developers and gives credit for art assets
    /// </summary>
    protected override void Interact()
    { 
        DialogueElement dialogue = new DialogueElement(null, "Covid Crossing Developers:\nLloyd Everett\nChelsea van Coller\nLynn Weyn", null, new DialogueElement("Next page" , "Covid Crossing art credit:\nTileset - PureAzuure\n(https://www.deviantart.com/pureazuure)\nCharacter art - Kenny RPG Urban Pack\n(https://www.kenney.nl/assets/rpg-urban-pack)", null, new DialogueTerminal("Close book")));
        SharedCanvas.Instance.dialogueManager.RunDialogue(dialogue);
    }
}

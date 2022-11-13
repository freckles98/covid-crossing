using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A behaviour for dialogue buttons. Index is assigned from the Unity scene and passed on to the click
/// handler on the dialogue manager. The index represents which button this is of those that are available.
/// </summary>
public class DialogueButton : MonoBehaviour
{
    public int index;

    /// <summary>
    /// Click handler for the button. Passes the click event to the dialogue manager with the button index.
    /// </summary>
    public void Clicked()
    {
        SharedCanvas.Instance.dialogueManager.ButtonClicked(index);
    }
}
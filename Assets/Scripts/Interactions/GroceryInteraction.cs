using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player is able to click on groceries to interact with them and remove them from the shelf.
/// </summary>
public class GroceryInteraction : Interaction
{
    /// <summary>
    /// By interacting with the groceries, the grocery dissapears and the players personal score increases.
    /// </summary>
    protected override void Interact()
    {
        SharedCanvas.Instance.scoreManager.ChangePersonalScore(5);
        gameObject.SetActive(false);  //remove self from scene
        
    }

}

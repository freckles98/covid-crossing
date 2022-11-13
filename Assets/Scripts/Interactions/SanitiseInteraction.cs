using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  SanitizeInteraction allows for the player to interact with the santizing stations, thus creating a brief period of immunity.
/// </summary>
public class SanitiseInteraction : Interaction
{
    public PlayerImmunityBehaviour playerImmunityBehaviour;
    /// <summary>
    /// The Interact method activates the players immunity.
    /// </summary>
    protected override void Interact()
    {
        playerImmunityBehaviour.activateImmunity();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player immunity after having interacted with the sanitising stations.
/// </summary>
public class PlayerImmunityBehaviour : MonoBehaviour
{
    private int immunityCounter; //for how long immune for 

    /// <summary>
    /// Checks if player is still immune.
    /// </summary>
    /// <returns>True if still immune, else false.</returns>
    public bool IsImmune()
    {
        return (immunityCounter > 0);
    }

    /// <summary>
    /// Activation of immunity, triggered by interacting with sanitising station.
    /// </summary>
    public void activateImmunity()
    {
        immunityCounter = 250;
    }


    /// <summary>
    /// If the player is still immune, it decrements the amount of time the player will still be immune for and sets the "blueness" of the player (as a visual indicator) based off of the time left.
    /// </summary>
    void FixedUpdate()
    {
        if (immunityCounter > 0) //if still immune
        {
            immunityCounter -= 1; //decrement timer
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1-0.5f*((immunityCounter)/250f), 1 - 0.5f * ((immunityCounter) / 250f), 1); //visual indicator
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1); 
        }

    }
}

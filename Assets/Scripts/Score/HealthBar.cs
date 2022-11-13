using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Controlls bar seen on screen. Generic for community and personal bar.
/// </summary>
public class HealthBar : MonoBehaviour 
{
    public Slider slider; 

    /// <summary>
    /// To be called by scoremanager, used to change slider value.
    /// </summary>
    /// <param name="change">Amount slider should change by.</param>
    public void ChangeHealth(float change) 
    {
        slider.value += change;
    }

    /// <summary>
    /// Resets slider value to 50. Called when resetting day.
    /// </summary>
    public void ResetHealth() //to be called when resetting day
    {
        slider.value = 30;
    }

    /// <summary>
    /// Retrieves value of slider.
    /// </summary>
    /// <returns>Value of slider.</returns>
    public float GetHealth()
    {
        return (float) slider.value;
    }
}

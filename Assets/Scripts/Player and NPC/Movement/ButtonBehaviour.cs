using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manages induvidual arrow buttons.
/// </summary>
public class ButtonBehaviour : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
 
    private bool isPressed;

    /// <summary>
    /// Getter for isPressed
    /// </summary>
    /// <returns>True if pressed, else false.</returns>
    public bool IsPressed()
    {
        return isPressed;
    }

    /// <summary>
    /// When button clicked/pressed, sets isPressed to true.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    /// <summary>
    /// When button is not being pressed, sets isPressed to false;
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        
    }

    /// <summary>
    /// Sets isPressed to false.
    /// </summary>
    public void Reset()
    {
        isPressed = false;
    }

  
}

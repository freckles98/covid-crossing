using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages checking which buttons are being pressed, and can reset all buttons.
/// </summary>
public class ButtonManager : MonoBehaviour
{
    public ButtonBehaviour LeftArrow;
    public ButtonBehaviour RightArrow;
    public ButtonBehaviour UpArrow;
    public ButtonBehaviour DownArrow;

    /// <summary>
    /// Checks if left arrow pressed.
    /// </summary>
    /// <returns>True if left arrow pressed, else false.</returns>
    public bool IsLeftArrowPressed()
    {
        return LeftArrow.IsPressed();
    }

    /// <summary>
    /// Checks if right arrow pressed.
    /// </summary>
    /// <returns>True if right arrow pressed, else false.</returns>
    public bool IsRightArrowPressed()
    {
        return RightArrow.IsPressed();
    }

    /// <summary>
    /// Checks if up arrow pressed.
    /// </summary>
    /// <returns>True if up arrow pressed, else false.</returns>
    public bool IsUpArrowPressed()
    {
        return UpArrow.IsPressed();
    }

    /// <summary>
    /// Checks if left arrow pressed.
    /// </summary>
    /// <returns>True if left arrow pressed, else false.</returns>
    public bool IsDownArrowPressed()
    {
        return DownArrow.IsPressed();
    }

    /// <summary>
    /// Resets all arrow buttons.
    /// </summary>
    public void Reset()
    {
        LeftArrow.Reset();
        RightArrow.Reset();
        UpArrow.Reset();
        DownArrow.Reset();
    }
}

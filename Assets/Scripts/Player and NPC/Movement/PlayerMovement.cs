using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages movement specific to the player.
/// </summary>
public class PlayerMovement : AbstractMovement
{
    private Vector2 localMovement;

    /// <summary>
    /// Resets arrow buttons.
    /// </summary>
    public override void Start()
    {
        SharedCanvas.Instance.buttonManager.Reset();
    }

    /// <summary>
    /// Gets movement speed specific to the player.
    /// </summary>
    /// <returns>Movement speed specific to the player.</returns>
    protected override float GetMoveSpeed()
    {
        return 5f;
    }

    /// <summary>
    /// Get movement vector used to move player sprite, based off of arrow button clicks.
    /// </summary>
    /// <returns>Movement vector used to move player sprite.</returns>
    protected override Vector2 GetMovementVector()
    {
        localMovement.x = 0.0f;
        if (SharedCanvas.Instance.buttonManager.IsLeftArrowPressed()) { localMovement.x = -1.0f; }
        else if (SharedCanvas.Instance.buttonManager.IsRightArrowPressed()) { localMovement.x = 1.0f; }
        else { localMovement.x = Input.GetAxisRaw("Horizontal"); }

        if (localMovement.x == 0.0f)
        {
            localMovement.y = 0.0f;
            if (SharedCanvas.Instance.buttonManager.IsDownArrowPressed()) { localMovement.y = -1.0f; }
            else if (SharedCanvas.Instance.buttonManager.IsUpArrowPressed()) { localMovement.y = 1.0f; }
            else { localMovement.y = Input.GetAxisRaw("Vertical"); }

        }
        else
        {
            localMovement.y = 0f;
        }
        return localMovement;
    }

    
}

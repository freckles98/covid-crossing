using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages movement specific to the NPC, directions and durations being chosen at random and checks in place to ensure NPC doesn't get stuck.
/// </summary>
public class NPCMovement : AbstractMovement
{
    private Vector2 localMovement; //x,y 
    private Vector2 lastPos;
    int howlong; //how long to move for

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start(); //super
        localMovement.x = 0;
        localMovement.y = 0;
        howlong = 0;
        lastPos = new Vector2(0, 0);

    }

    /// <summary>
    /// Gets movement speed specific to the NPC.
    /// </summary>
    /// <returns>Movement speed specific to the NPC.</returns>
    protected override float GetMoveSpeed()
    {
        return 4f;
    }

    /// <summary>
    /// Get movement vector used to move NPC sprite, Direction and duration chosen at random.
    /// </summary>
    /// <returns>Movement vector used to move NPC sprite.</returns>
    protected override Vector2 GetMovementVector()
    {
        if (howlong <= 0)
        {
            int direction = Random.Range(0, 5); //0,1,2,3,4 for left right up down, nothing
            switch (direction)
            {
                case 0:
                    localMovement.x = -1;
                    localMovement.y = 0;
                    howlong = Random.Range(50, 500); 
                    break;
                case 1:
                    localMovement.x = 1;
                    localMovement.y = 0;
                    howlong = Random.Range(50, 500); 
                    break;
                case 2:
                    localMovement.x = 0;
                    localMovement.y = 1;
                    howlong = Random.Range(50, 500); 
                    break;
                case 3:
                    localMovement.x = 0;
                    localMovement.y = -1;
                    howlong = Random.Range(50, 500); 
                    break;
                case 4:
                    localMovement.x = 0;
                    localMovement.y = 0;
                    howlong = Random.Range(50, 150); //shorter time standing and doing nothing than walking.
                    break;
            }
        }

        return localMovement;
    }

    /// <summary>
    /// Decrements duration of direction, and checks if NPC is stuck (e.g. walking into a wall), if so picks a new direction. Then calls parent's FixedUodate.
    /// </summary>
    public override void FixedUpdate()
    {
        howlong -= 1;

        //if stuck, change direction:
        Vector2 currentPos = rb.position;
        if (currentPos == lastPos && (localMovement.x != 0 || localMovement.y != 0)) //stuck and not deliberately standing still
        {
            howlong = 0; //so new direction chosen in update
            base.Update();
        }
        lastPos = currentPos; //for next iteration

        base.FixedUpdate();
    }

}

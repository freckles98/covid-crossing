using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for player and NPC movement. Moves sprite across screen, sets animation variables, and puts the mask in the correct direction.
/// </summary>
public abstract class AbstractMovement : MonoBehaviour
{
    public float moveSpeed; 

    public Rigidbody2D rb;

    protected Vector2 movement; //x,y 

    private Vector2 lastMovement;

    public Animator animator;

    //mask stuff:
    public GameObject mask;
    public Sprite left_mask;
    public Sprite right_mask;
    public Sprite forward_mask;
    public Sprite back_mask;

    /// <summary>
    /// Gets speed sprite should move by from child class, and initial last movement to down.
    /// </summary>
    public virtual void Start()
    {
        moveSpeed = GetMoveSpeed(); //different for NPC and player
        lastMovement.x = 0f;
        lastMovement.y = -1f;
    }

    /// <summary>
    /// Gets movement vector from child if there is no dialogue or setup happening, and sets animation variables and mask positions accordingly.
    /// </summary>
    protected void Update()
    {
        if (!SharedCanvas.Instance.dialogueManager.IsActive) //no dialogue on screen
        {
            if (!SharedCanvas.Instance.setupManager.settingUp) //not in setup
            {
                movement = GetMovementVector();
            }
            
        }
        else
        {
            movement.x = 0f;
            movement.y = 0f;
        }

        if (movement.x != 0 || movement.y != 0) //if moving 
        {
            lastMovement = movement; // should be last non idle movement
        } //else dont update last movement

        //set animation variables

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("LastMovementHorizontal", lastMovement.x);
        animator.SetFloat("LastMovementVertical", lastMovement.y);
        animator.SetFloat("Speed", movement.magnitude);


        //set mask to face correct direction:
        if (movement.x < -0.01) //left
        {
            mask.GetComponent<SpriteRenderer>().sprite = left_mask;
            mask.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (movement.x > 0.01) //right
        {
            mask.GetComponent<SpriteRenderer>().sprite = right_mask;
            mask.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (movement.y > 0.01) //up
        {
            mask.GetComponent<SpriteRenderer>().sprite = back_mask;
            mask.transform.localPosition = new Vector3(0, -0.05f, 0);
        }
        else if (movement.y < -0.01) //down 
        {
            mask.GetComponent<SpriteRenderer>().sprite = forward_mask;
            mask.transform.localPosition = new Vector3(0, -0.05f, 0);
        }
        else //standing still
        {
            mask.transform.localPosition = new Vector3(0, -0.08f, 0);
        }
    }

    /// <summary>
    /// Moves sprite on screen.
    /// </summary>
    public virtual void FixedUpdate() 
    {
         rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// To be overridden by child class. Retrieve movement speed sprite should move by.
    /// </summary>
    /// <returns>Speed by which sprite should move.</returns>
    protected abstract float GetMoveSpeed();

    /// <summary>
    /// To be overridden by child class. Retrieve movement vector used to move sprite.
    /// </summary>
    /// <returns>Movement vector used to move sprite.</returns>
    protected abstract Vector2 GetMovementVector();
}

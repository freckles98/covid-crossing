using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    private Vector2 movement;

    private Vector2 lastMovement;

    public Animator animator;

    void Start()
    {   
        lastMovement.x = 0f;
        lastMovement.y = -1f;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x > -0.01f && movement.x < 0.01f)
        {
            movement.y = Input.GetAxisRaw("Vertical");
            if (movement.y > -0.01f && movement.y < 0.01f)
            {
                // Don't update lastMovement
            }
            else
            {
                lastMovement.x = 0f;
                lastMovement.y = movement.y;
            }
        }
        else
        {
            lastMovement.x = movement.x;
            lastMovement.y = 0f;
            movement.y = 0f;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("LastMovementHorizontal", lastMovement.x);
        animator.SetFloat("LastMovementVertical", lastMovement.y);
        animator.SetFloat("Speed", movement.magnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}

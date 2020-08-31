using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{
    // Movement
    int playerDirection = 1;
    float Xvelocity, Yvelocity;

    // Jump
    int maxJumps = 2;
    int jumpCount = 0;
    bool jumpButtonPressed = false;
    bool canJump;

    // Crouch
    int crouchFactor = 350;
    bool canCrouch = true;
    bool crouching = false;
    BoxCollider2D prevPlayerBC;
    Vector2 prevScale = new Vector2(.06f, .2f);

    private void Start()
    {
        prevPlayerBC = playerBC;
    }

    void Update()
    {
        // Player Movement
        Movement();
        if (Input.GetKeyDown(KeyCode.R))
            GameManager.Instance.RestartLevel();
        //Debug.Log("Current State: " + currentState);
        //Debug.Log("player X velocity: " + playerRB.velocity.x);

        if (lastSavedPosition == null)
            lastSavedPosition = transform;
    }

    void Movement()
    {
        // Input detection
#if UNITY_EDITOR
        Xvelocity = Input.GetAxis("Horizontal");
        Yvelocity = Input.GetAxis("Vertical");
#endif

#if UNITY_ANDROID
        Xvelocity = movementJoystick.Horizontal;
        Yvelocity = movementJoystick.Vertical;
#endif

        // Add speed to velocity to move
        playerRB.velocity = new Vector2(Xvelocity * speed, playerRB.velocity.y);

        // Jump
#if UNITY_EDITOR    
        if (Input.GetKeyDown(jumpKey) && !GetCrouchButtonPressed())
        {
            if (OnGround())
            {
                Jump();
            }
            else
            {
                if (jumpCount < maxJumps)
                {
                    Jump();
                }
            }
        }
#endif

#if UNITY_ANDROID
        if (jumpButtonPressed == true && !GetCrouchButtonPressed())
        {
            if (OnGround())
            {
                Jump();
            }
            else
            {
                if (jumpCount < maxJumps)
                {
                    Jump();
                }
            }
        }
#endif

        // Crouch
        if (Yvelocity < -.2f && OnGround())
        {
            Crouch();
        }
        else GetUp();

        // Current Direction
        if (Xvelocity < 0 && playerDirection == 1)
        {
            InvertDirection();
        }
        if (Xvelocity > 0 && playerDirection == 0)
        {
            InvertDirection();
        }

        //Check states
        CheckStates();
    }

    void Jump()
    {
        // Prevent infinite jumping
        if (OnGround()) jumpCount = 0;

        // Add vertical force to jump
        if (jumpCount < 1)
        {
            playerRB.AddForce(Vector2.up * jumpSpeed);
        }
        else if (jumpCount >= 1) playerRB.AddForce(Vector2.up * (jumpSpeed + jumpSpeed * .15f));
        {
            jumpCount++;
        }

        jumpButtonPressed = false;
    }

    void Crouch()
    {
#if UNITY_EDITOR
        if (GetCrouchButtonPressed() && canCrouch)
        {
            DoCrouch();
        }

        if (!GetCrouchButtonPressed() && !canCrouch)
        {
            GetUp();
        }
#endif

#if UNITY_ANDROID
        if (GetCrouchButtonPressed() && canCrouch)
        {
            DoCrouch();
        }
        if (!GetCrouchButtonPressed() && !canCrouch)
        {
            GetUp();
        }
#endif
    }

    void DoCrouch()
    {
        // Prevent floating during crouch
        playerRB.AddForce(Vector2.down * crouchFactor);

        // Change Player Scale and BoxCollider size
        transform.localScale = new Vector2(transform.localScale.x, .1f);
        playerBC.size = new Vector2(playerBC.size.x, 4.07f);
        playerBC.offset = new Vector2(playerBC.offset.x, -0.007f);

        canCrouch = false;
    }

    void GetUp()
    {
        // Revert crouch effects
        transform.localScale = new Vector2(transform.localScale.x, prevScale.y);
        playerBC.size = new Vector2(prevPlayerBC.size.x, prevPlayerBC.size.y);
        playerBC.offset = new Vector2(prevPlayerBC.offset.x, prevPlayerBC.offset.y);

        canCrouch = true;
    }

    void InvertDirection()
    {
        //Detect previous direction
        int newDirection = playerDirection == 1 ? 0 : 1;
        playerDirection = newDirection;

        // Change Scale to face new direction
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    // Boxcast to check if on ground
    bool OnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(playerBC.bounds.center, playerBC.bounds.size, 0, Vector2.down, .1f, GroundLayer);
        return hit.collider != null;
    }

    public void GetJumpButtonPressed()
    {
        jumpButtonPressed = true;
    }

    public bool GetCrouchButtonPressed()
    {
        if (movementJoystick.Direction.y < crouchingDistance)
        {
            return crouching = true;
        }

        if (movementJoystick.Direction.y > crouchingDistance)
        {
            return crouching = false;
        }

        return false;
    }

    void CheckStates()
    {
        if(OnGround())
        {
            if(crouching)
            {
                if (playerRB.velocity.x < -0.5f && playerRB.velocity.x > 0.5f)
                {
                    //Debug.Log("Crouch Walk State");
                    playerStates.ChangeState(State.CrouchWalking);
                }
                else
                {
                    //Debug.Log("Crouch State");
                    playerStates.ChangeState(State.Crouching);
                }
            }
            else
            {
                if (playerRB.velocity.x != 0)
                {
                    //Debug.Log("Walk State");
                    playerStates.ChangeState(State.Walking);
                }
                else
                {
                    //Debug.Log("Idle State");
                    playerStates.ChangeState(State.Idle);
                }
            }
        }
        else
        {
            if(playerRB.velocity.y > 0)
            {
                if (jumpCount == 1)
                {
                    //Debug.Log("Jump State");
                    playerStates.ChangeState(State.Jumping);
                }

                if (jumpCount == 2)
                {
                    //Debug.Log("Double Jump State");
                    playerStates.ChangeState(State.DoubleJumping);
                }
            }
            else
            {
                //Debug.Log("Fall State");
                playerStates.ChangeState(State.Falling);
            }
        }
    }
}
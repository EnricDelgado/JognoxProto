using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : Player
{
    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                OnIdle();
                break;

            case State.Walking:
                OnWalk();
                break;

            case State.Jumping:
                OnJump();
                break;

            case State.DoubleJumping:
                OnDoubleJump();
                break;

            case State.Falling:
                OnFall();
                break;

            case State.Crouching:
                OnCrouch();
                break;

            case State.CrouchWalking:
                OnCrouchWalk();
                break;

            case State.Hurting:
                OnHurt();
                break;

            case State.Dying:
                OnDie();
                break;
        }
    }

    public void ChangeState(State newState)
    {
        switch (currentState)
        {
            case State.Idle:
                playerAnimator.SetBool("onIdle", false);
                break;

            case State.Walking:
                playerAnimator.SetBool("onWalk", false);
                break;

            case State.Falling:
                playerAnimator.SetBool("onFall", false);
                break;

            case State.Crouching:
                playerAnimator.SetBool("onCrouch", false);
                break;

            case State.CrouchWalking:
                playerAnimator.SetBool("onCrouchWalk", false);
                break;
        }

        currentState = newState;
    }

    void OnIdle()
    {
        playerAnimator.SetBool("onIdle", true);
    }

    void OnWalk()
    {
        playerAnimator.SetBool("onWalk", true);
    }

    void OnJump()
    {
        playerAnimator.SetTrigger("onJump");
    }

    void OnDoubleJump()
    {
        playerAnimator.SetTrigger("onDoubleJump");
    }

    void OnFall()
    {
        playerAnimator.SetBool("onFall", true);
    }

    void OnCrouch()
    {
        playerAnimator.SetBool("onCrouch", true);
    }

    void OnCrouchWalk()
    {
        playerAnimator.SetBool("onCrouchWalk", true);
    }

    void OnHurt()
    {
        playerAnimator.SetTrigger("onHurt");
    }

    void OnDie()
    {
        playerAnimator.SetTrigger("onDie");
    }
}
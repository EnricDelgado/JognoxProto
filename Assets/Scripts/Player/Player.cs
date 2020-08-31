using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerRestart))]
public class Player : MonoBehaviour, IKillable, IDamagable<float>, IRestartable
{
    [Header("Stats")]
    public int health = 3;
    public int maxHealth = 3;
    public float speed = 7;
    public float jumpSpeed = 150;
    public float crouchingDistance = -.4f;
    public float damageDealed = 1;
    public float damageTaken = 1;
    public float repulsionFactor = 1500;
    public int coins = 0;
    public Transform lastSavedPosition;

    [HideInInspector] public enum State { Idle, Walking, Jumping, DoubleJumping, Falling, Crouching, CrouchWalking, Hurting, Dying }
    [Header("State Machine")]
    public State currentState = State.Idle;

    [Header("Player Components")]
    public Rigidbody2D playerRB;
    public BoxCollider2D playerBC;
    public Animator playerAnimator;
    public PlayerStates playerStates;


    [Header("Inputs")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.DownArrow;

    [Header("Interaction Layers")]
    public LayerMask GroundLayer;

    [Header("Joysticks")]
    public Joystick movementJoystick;

    public virtual void Die()
    {
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void TakeDamage(float damage)
    {
        health -= 1;
        GetComponent<PlayerController>().LoseHealth();
    }

    public virtual void Restart()
    {
        transform.position = lastSavedPosition.position;
        health = maxHealth;
        playerStates.ChangeState(Player.State.Idle);

        Debug.Log("player restarted");
    }
}

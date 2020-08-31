using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Enemy
{
    enum State { Idle, Patrol, Seek, Attack, Damage, Die}
    State currentState = State.Idle;

    public Transform patrolPoint1, patrolPoint2;

    void Update()
    {
        if(enemyType == EnemyType.Weak)
        {
            WeakEnemyMovement();
        }
        else if(enemyType == EnemyType.Flying)
        {
            FlyingEnemyMovement();
        }
        else if(enemyType == EnemyType.Heavy)
        {
            HeavyEnemyMovement();
        }
    }

    void WeakEnemyMovement()
    {
        //transform.position = Vector3.Lerp(patrolPoint1.position, patrolPoint2.position, Mathf.PingPong(Time.time * enemySpeed, 1.0f));

        transform.position = Vector3.Lerp(patrolPoint1.position, patrolPoint2.position, (Mathf.Sin(enemySpeed * Time.time) + 1.0f) / 2.0f);
    }

    void FlyingEnemyMovement()
    {

    }

    void HeavyEnemyMovement()
    {

    }
}

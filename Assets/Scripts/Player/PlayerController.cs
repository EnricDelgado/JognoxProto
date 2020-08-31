using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Player
{
    private float topAngle;
    private float sideAngle;

    public GameObject live1, live2, live3;

    void Start()
    {
        Vector2 size = playerBC.size;
        size = Vector2.Scale(size, (Vector2)transform.localScale);

        topAngle = Mathf.Atan(size.x / size.y) * Mathf.Rad2Deg;
        sideAngle = 90.0f - topAngle;

        GameManager.Instance.AddRestartableElement(GetComponent<PlayerRestart>());

        //Debug.Log("Top Angle: " + topAngle + ", " + "Side Angle: " + sideAngle);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (health == maxHealth)
        {
            for (int i = 0; i < lives.Length; i++)
                lives[i].GetComponent<Image>().enabled = true;
        }

        LoseHealth();*/
        Debug.Log(health);
    }

    public override void Die()
    {
        health = 0;

        playerStates.ChangeState(State.Dying);
        GameManager.Instance.GetUI().OnGameOver();
    }


    public void GetHealth()
    {
        if(health == 2) live3.GetComponent<Image>().enabled = true;
        else if (health == 1) live2.GetComponent<Image>().enabled = true;

        health += 1;
    }

    public void LoseHealth()
    {
        if (health == 2) { live3.GetComponent<Image>().enabled = false; }
        else if (health == 1) { live2.GetComponent<Image>().enabled = false; }
        else if (health == 0) { live1.GetComponent<Image>().enabled = false; Die(); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Vector3 v = (Vector3)collision.contacts[0].point - transform.position;

            if (Vector3.Angle(v, transform.up) <= topAngle)
            {
                //Debug.Log("Collision on top");
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damageDealed);
            }
            else if (Vector3.Angle(v, transform.right) <= sideAngle)
            {
                //Debug.Log("Collision on right");
                //Debug.Log("Take Damage Right");
                playerStates.ChangeState(State.Hurting);
                DamageRepulsion("right");
                TakeDamage(damageTaken);
            }
            else if (Vector3.Angle(v, -transform.right) <= sideAngle)
            {
                //Debug.Log("Collision on Left");
                //Debug.Log("Take Damage left");
                playerStates.ChangeState(State.Hurting);
                DamageRepulsion("left");
                TakeDamage(damageTaken);
            }
            else
            {
                //Debug.Log("Collision on Bottom");
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damageDealed);
            }
        }
    }

    void DamageRepulsion(string direction)
    {
        if(direction == "left")
        {
            playerRB.AddForce(Vector2.right * repulsionFactor);
            //Debug.Log("Add force to right");
        }
        else if(direction == "right")
        {
            //Debug.Log("Add force to left");
            playerRB.AddForce(Vector2.right * repulsionFactor);
        }
    }
}
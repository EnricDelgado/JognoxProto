using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(CollectableRestart))]
public class Collectables : MonoBehaviour, ICollectable
{
    public enum Type { Coin, Health, SpeedPowerUp, JumpPowerUp, HealthPowerUp }
    [Header("ItemType")]
    public Type itemType;

    [Header("ItemStats")]
    public int healthAmount = 1;
    public int coinAmount = 1;
    public float speedUpAmount = 1.5f;
    public float jumpUpAmount = 1.3f;
    public float powerUpTimer = 5f;

    void Start()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
        GameManager.Instance.AddRestartableElement(GetComponent<CollectableRestart>());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (itemType == Type.Health)
            {
                if (GameManager.Instance.GetPlayer().GetComponent<PlayerController>().health < GameManager.Instance.GetPlayer().maxHealth)
                    OnCollectItem();
                else
                    Debug.Log("Health full");
                return;
            }
            else if (itemType == Type.Coin)
            {
                OnCollectItem();
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                StartCoroutine(OnPowerUpCollected());
            }
        }
    }

    public IEnumerator OnPowerUpCollected()
    {
        OnStartPowerUp();
        yield return new WaitForSeconds(powerUpTimer);
        OnEndPowerUp();
    }

    public virtual void OnStartPowerUp() { }

    public virtual void OnEndPowerUp() 
    {
        DisableItem();
    }

    public virtual void OnCollectItem()
    {
        DisableItem();
    }

    void DisableItem()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}

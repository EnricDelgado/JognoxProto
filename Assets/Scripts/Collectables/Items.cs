using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : Collectables
{
    public override void OnCollectItem()
    {
        base.OnCollectItem();

        if(itemType == Type.Health)
        {
            GameManager.Instance.GetPlayer().GetComponent<PlayerController>().GetHealth();
        }
        if(itemType == Type.Coin)
        {
            GameManager.Instance.GetPlayer().coins += 1;
        }
    }
}

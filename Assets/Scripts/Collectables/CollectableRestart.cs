using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableRestart : RestartManager
{
    public override void Restart()
    {
        base.Restart();

        Collectables[] collectables = GameManager.Instance.GetCollectables();

        for(int i = 0; i < collectables.Length; i++)
        {
            collectables[i].GetComponent<SpriteRenderer>().enabled = true;
            collectables[i].GetComponent<CircleCollider2D>().enabled = true;
        }

        Debug.Log("Collectables restarted");
    }
}

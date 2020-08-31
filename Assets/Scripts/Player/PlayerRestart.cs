using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRestart : RestartManager
{
    public override void Restart()
    {
        base.Restart();
        Player player = GameManager.Instance.GetPlayer();

        transform.position = player.lastSavedPosition.position;
        player.health = player.maxHealth;
        player.playerStates.ChangeState(Player.State.Idle);

        Debug.Log("player restarted");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : Collectables
{
    float prevDamageTaken;
    float prevJumpPower;
    float prevSpeed;

    public override void OnStartPowerUp()
    {
        Player player = GameManager.Instance.GetPlayer();

        if (itemType == Type.HealthPowerUp)
        {
            prevDamageTaken = player.damageTaken;
            player.damageTaken = 0;
        }

        if (itemType == Type.SpeedPowerUp)
        {
            prevSpeed = player.speed;
            player.speed *= speedUpAmount;
        }

        if (itemType == Type.JumpPowerUp)
        {
            prevJumpPower = player.jumpSpeed;
            player.jumpSpeed *= jumpUpAmount;
        }
    }

    public override void OnEndPowerUp()
    {
        base.OnEndPowerUp();

        Player player = GameManager.Instance.GetPlayer();

        if (itemType == Type.HealthPowerUp)
        {
            player.damageTaken = prevDamageTaken;
        }

        if (itemType == Type.SpeedPowerUp)
        {
            player.speed = prevSpeed;
        }

        if (itemType == Type.JumpPowerUp)
        {
            player.jumpSpeed = prevJumpPower;
        }
    }
}

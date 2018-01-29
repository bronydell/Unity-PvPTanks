using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBonus : BaseBonus
{

    public override void Effect()
    {
        foreach (var player in FindObjectsOfType<Player>())
        {
            if (player != Player)
            {
                player.MovementController.CanMove = false;
            }
        }
    }

    public override void Deffect()
    {
        foreach (var player in FindObjectsOfType<Player>())
        {
            player.MovementController.CanMove = true;
        }
    }
}

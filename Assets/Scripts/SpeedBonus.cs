using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : BaseBonus {

    public override void Effect()
    {
        Player.MovementController.MaxSpeed *= 2;
        Player.MovementController.ShootCooldown /= 2;
    }

    public override void Deffect()
    {
        Player.MovementController.MaxSpeed /= 2;
        Player.MovementController.ShootCooldown *= 2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBonus : BaseBonus
{

    public override void Effect()
    {
        Player.Lives++;
    }

    public override void Deffect()
    {
        // (⌐■_■)–︻╦╤─ --(STAY RIGHT HERE!)
    }
}


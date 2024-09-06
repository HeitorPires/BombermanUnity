using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStrenght : PowerUpBase
{
    protected override void OnCollect()
    {
        SOStats.ExplosionStrenght++;
        base.OnCollect();
    }
}

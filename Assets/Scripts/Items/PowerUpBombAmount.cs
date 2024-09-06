using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBombAmount : PowerUpBase
{
    protected override void OnCollect()
    {
        SOStats.IncrementBombAmount();
        base.OnCollect();
    }
}

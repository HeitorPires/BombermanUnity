using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUpBase
{
    protected override void OnCollect()
    {
        SOStats.Speed += 1f;
        base.OnCollect();
    }
}

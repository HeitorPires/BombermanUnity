using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOStats : ScriptableObject
{
    public float Speed = 1;
    public int ExplosionStrenght = 1;
    public Action OnBombAmountIncrement;
    public int BombAmount { get; private set; } = 1;

    public void IncrementBombAmount()
    {
        OnBombAmountIncrement?.Invoke();
        BombAmount++;
    }

    public void ResetSO()
    {
        Speed = 1;
        ExplosionStrenght = 1;
        BombAmount = 1;
    }


}

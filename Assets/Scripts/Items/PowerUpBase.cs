using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    public SOStats SOStats;
    public LayerMask Layer;


    protected virtual void OnCollect()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & Layer) != 0)
        {
            OnCollect();
        }
    }

}

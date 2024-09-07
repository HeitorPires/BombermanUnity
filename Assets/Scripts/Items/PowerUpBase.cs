using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    public SOStats SOStats;
    public LayerMask LayerPlayer;
    public LayerMask LayerExplosion;


    protected virtual void OnCollect()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & LayerPlayer) != 0)
        {
            OnCollect();
        }
        
        if (((1 << collision.gameObject.layer) & LayerExplosion) != 0)
        {
            Destroy(gameObject);
        }
    }

}

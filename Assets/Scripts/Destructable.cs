using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public float DestructableTime = 1f;
    private void Start()
    {
        Destroy(gameObject, DestructableTime);
    }
}

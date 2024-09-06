using UnityEngine;

public class Destructable : MonoBehaviour
{
    public float DestructableTime = 1f;

    private void Start()
    {
        Destroy(gameObject, DestructableTime);
    }

    private void OnDestroy()
    {
        
    }
}

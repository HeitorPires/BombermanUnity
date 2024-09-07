using UnityEngine;

public class Destructable : MonoBehaviour
{
    public float DestructableTime = 1f;
    public PowerUpSpawnerSO PowerUpSpawner;

    private void Start()
    {
        Destroy(gameObject, DestructableTime);
    }

    private void OnDestroy()
    {
        PowerUpSpawner.SpawnPowerUp(transform.position);
    }
}

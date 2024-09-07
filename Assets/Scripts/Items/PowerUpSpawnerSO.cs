using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PowerUpSpawnerSO : ScriptableObject
{
    [Range(0f, 1f)]
    public float SpawnChance = .25f;
    [SerializeField] private List<PowerUpBase> powerUpsToSpawn;

    public void SpawnPowerUp(Vector3 position)
    {
        if (powerUpsToSpawn.Count < 0) return;
        float random = Random.Range(0f, 1f);
        if (random <= SpawnChance)
        {
            var powerUp = Instantiate(powerUpsToSpawn[Random.Range(0, powerUpsToSpawn.Count)], position, Quaternion.identity);
        }
    }
}


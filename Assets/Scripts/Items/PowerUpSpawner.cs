using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class PowerUpSpawner : Singleton<PowerUpSpawner>
{
    [Range(0f, 1f)]
    [SerializeField] private float _spawnChance = .25f;
    [SerializeField] private List<PowerUpBase> powerUpsToSpawn;

    public void SpawnPowerUp(Vector3 position)
    {
        if (powerUpsToSpawn.Count < 0) return;
        float random = Random.Range(0f, 1f);
        if (random <= _spawnChance)
        {
            var powerUp = Instantiate(powerUpsToSpawn[Random.Range(0, powerUpsToSpawn.Count)], position, Quaternion.identity);
        }
    }
}

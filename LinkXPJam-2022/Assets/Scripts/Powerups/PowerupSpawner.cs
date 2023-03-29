using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<GameObject> powerups = new();

    [Header("settings")]
    [SerializeField] float minSpawnDelay = 10f;
    [SerializeField] float maxSpawnDelay = 20f;
    [SerializeField] float minSpawnRange = 10f;
    [SerializeField] float maxSpawnRange = 15f;

    private void Start()
    {
        StartCoroutine(SpawnPowerupsCo());
    }

    IEnumerator SpawnPowerupsCo()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        SpawnPowerup();
        StartCoroutine(SpawnPowerupsCo());
    }

    void SpawnPowerup()
    {
        Transform t = Instantiate(powerups[Random.Range(0, powerups.Count)]).transform;
        t.position = GetSpawnLocation();
    }

    Vector2 GetSpawnLocation()
    {
        Vector2 pos = player.position;
        pos += Random.insideUnitCircle * Random.Range(minSpawnRange, maxSpawnRange);
        return pos;
    }
}
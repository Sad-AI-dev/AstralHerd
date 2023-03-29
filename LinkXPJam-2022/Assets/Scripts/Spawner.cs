using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform spawnPos;

    [SerializeField] float minSpawnDelay = 0f, maxSpawnDelay = 0.5f;
    [SerializeField] float spawnCount;

    int spawnedCount = 0;

    private void Start()
    {
        //test
        Execute();
    }
    public void Execute()
    {
        StartCoroutine(SpawnCo());
    }

    IEnumerator SpawnCo()
    {
        SpawnObject();
        spawnedCount++;
        if (spawnedCount < spawnCount) { //spawn next object
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            StartCoroutine(SpawnCo());
        }
    }

    void SpawnObject()
    {
        Instantiate(prefab);
    }
}

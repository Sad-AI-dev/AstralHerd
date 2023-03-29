using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPatterns : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float spawnRange = 1f;
    [SerializeField] float clusterSpawnrange = 1f;

    public void SpawnAroundTarget(int count = 1)
    {
        for (int i = 0; i < count; i++) {
            //get target
            Transform target = PathManager.instance.GetRandomFollower();
            if (target != null) {
                Vector2 spawnPos = (Vector2)target.position + (Random.insideUnitCircle.normalized * spawnRange);
                //spawn prefab
                Transform enemy = Instantiate(prefab).transform;
                enemy.position = spawnPos;
            }
        }
    }

    public void ClusterSpawn(int count = 1)
    {
        //get position
        Vector2 clusterPos = (Vector2)PathManager.instance.GetRandomFollower().position + (Random.insideUnitCircle.normalized * spawnRange);
        for (int i = 0; i < count; i++) {
            Vector2 offset = Random.insideUnitCircle.normalized * Random.Range(0, clusterSpawnrange);
            //spawn prefab
            Transform enemy = Instantiate(prefab).transform;
            enemy.position = clusterPos + offset;
        }
    }
}

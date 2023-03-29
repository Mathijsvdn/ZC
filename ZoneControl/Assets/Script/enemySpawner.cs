using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public float spawnRadius;
    public GameObject enemyPrefab;
    public float spawnInterval;
    public bool spawnOnStart;

    private float spawnTimer;
    private bool doSpawn;
    private int enemiesInZone;

    public void StartSpawning()
    {
        doSpawn = true;
    }

    private void Start()
    {
        if (spawnOnStart)
        {
            spawnTimer = Time.time + spawnInterval;
            StartSpawning();
        }
    }

    public void StopSpawning()
    {
        doSpawn = false;
    }

    private void Update()
    {
        if (!doSpawn)
            return;

        if (Time.time >= spawnTimer)
        {
            spawnTimer = Time.time + spawnInterval;
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            RaycastHit hit;
            if (Physics.Raycast(enemyInstance.transform.position, -enemyInstance.transform.up, out hit))
            {
                enemyInstance.transform.position = hit.point + new Vector3 (0f, .5f, 0f);
            }

            enemiesInZone++;
        }
    }

    public void RemoveEnemy()
    {
        enemiesInZone--;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }


}

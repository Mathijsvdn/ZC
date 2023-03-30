using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemySpawner : MonoBehaviour
{
    public float spawnRadius;
    public GameObject enemyPrefab;
    public float spawnInterval;
    public bool spawnOnStart;
    public Slider dangerMeter;
    public float maxEnemies;
    public GameObject associatedPortal;

    private float spawnTimer;
    private bool doSpawn;
    private float enemiesInZone;
    float enemyPercentage;

    public void StartSpawning()
    {
        doSpawn = true;
    }

    private void Awake()
    {
        if (spawnOnStart)
        {
            spawnTimer = Time.time + spawnInterval;
            StartSpawning();
        }

        dangerMeter.maxValue = 1;
    }

    public void StopSpawning()
    {
        doSpawn = false;
    }

    private void Update()
    {
        enemyPercentage = enemiesInZone / maxEnemies;
        dangerMeter.value = enemyPercentage;
        if (!doSpawn)
            return;

        if (enemiesInZone < maxEnemies)
        {
            if (Time.time >= spawnTimer)
            {
                spawnTimer = Time.time + spawnInterval;
                Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;

                RaycastHit hit;
                if (Physics.Raycast(spawnPosition, -Vector3.up, out hit))
                {
                    spawnPosition = hit.point + new Vector3(0f, .7f, 0f);
                }

                GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemiesInZone++;
            }
        }
        else if (enemiesInZone >= maxEnemies)
        {
            associatedPortal.SetActive(false);
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

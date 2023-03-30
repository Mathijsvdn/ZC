using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public float detectionDistance;
    public Transform player;
    public float health;
    public float attackRange;
    public float attackRate;
    public int droppedGel;

    private bool spottedPlayer;
    private float attackCooldown;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionDistance)
        {
            spottedPlayer = true;
        }

        if (spottedPlayer)
            agent.SetDestination(player.position);

        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (Time.time >= attackCooldown)
            {
                attackCooldown = Time.time + attackRate;
                player.GetComponent<PlayerHealth>().TakeDamage(2);
            }
        }
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            FindObjectOfType<enemySpawner>().RemoveEnemy();
            FindObjectOfType<NPCshop>().gelVanMma += droppedGel;
            Destroy(gameObject);
        }
    }

   
}

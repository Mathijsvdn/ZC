using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public List<EnemyAi> enemiesInCombat = new List<EnemyAi>();
    public float dealtDamage;
    public Slider playerHealth;
    public GameObject portalA;
    public GameObject portalB;
    public GameObject portalC;
    public GameObject gameOver;

    private InputManager inputAttack;

    private void Awake()
    {
        inputAttack = new InputManager();
        inputAttack.Enable();

        playerHealth.maxValue = health;
    }
    private void Update()
    {
        if (inputAttack.Player.Attack.WasPerformedThisFrame())
        {
            foreach (EnemyAi target in enemiesInCombat)
            {
                target.TakeDamage(dealtDamage);
                enemiesInCombat.Remove(target);
                return;
            }
        }

        playerHealth.value = health;

        if (!portalA.activeSelf && !portalB.activeSelf && !portalC.activeSelf)
        {
            gameOver.SetActive(true);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            gameOver.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyAi aI = other.GetComponent<EnemyAi>();
        enemiesInCombat.Add(aI);
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyAi aI = other.GetComponent<EnemyAi>();
        enemiesInCombat.Remove(aI);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStart : MonoBehaviour
{
    public int playerHealth = 20;
    public int enemyHealth = 20;
    int enemyPower;
    int playerPower;
    public int randVar;
    public GameObject healthIcon;
    public GameObject attackIcon;
    public HealthBar enemyHealthBar;
    public HealthBar playerHealthBar;

    public void Start()
    {
        attackIcon.SetActive(false);
        healthIcon.SetActive(false);
        randVar = Random.Range(0, 2);
    }

    public void PlayerAttacks() // Player turn of Attack
    {
        
        if (playerHealth > 0)
        {
            playerPower = Random.Range(4, 6);
            enemyPower = Random.Range(4, 6);
            enemyHealth -= playerPower;
            EnemyTurn();
        }
    }
    public void PlayerHeals() //Player turn of Healing
    {
        if (playerHealth > 0)
        {
            playerPower = Random.Range(4, 6);
            enemyPower = Random.Range(4, 6);
            playerHealth += 5;
            playerHealthBar.SetHealth(playerHealth);
            EnemyTurn();
        }
    }

    public void EnemyTurn() //Enemy Turn
    {
        if (enemyHealth > 0) 
        {
            if(randVar == 1)
            {
                playerHealth -= enemyPower;
                randVar = Random.Range(0, 2);
                Debug.Log("Enemy Attacked");
            }
            else
            {
                enemyHealth += 3;
                randVar = Random.Range(0, 2);
                Debug.Log("Enemy Healed");
            }
            enemyHealthBar.SetHealth(enemyHealth);
            playerHealthBar.SetHealth(playerHealth);
        }
    }

    public void Update()
    {
        if (randVar == 1)
        {
            attackIcon.SetActive(true);
            healthIcon.SetActive(false);
        }
        else
        {
            attackIcon.SetActive(false);
            healthIcon.SetActive(true);
        }

        if (playerHealth <= 0)
        {
            print("Game Lost!");
            Debug.Log("Enemy Health: " + enemyHealth);
            this.enabled = false;

        }
        
        if (enemyHealth <= 0)
        {
            print("Game Won!");
            Debug.Log("Player Health: " + playerHealth);
            this.enabled = false;
        }

        if (playerHealth >= 20)
        {
            GameObject.Find("Defend").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("Defend").GetComponent<Button>().interactable = true;
        }

        if (enemyHealth >= 20)
        {
            randVar = 1;
        }
    }


}

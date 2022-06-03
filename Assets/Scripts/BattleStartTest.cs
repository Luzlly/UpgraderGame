using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleStartTest : MonoBehaviour
{
    public int playerHealth;
    int playerMaxHealth;
    public int enemyHealth;
    int enemyMaxHealth;
    int enemyPower;
    int playerPower;
    public int randVar;
    public GameObject healthIcon;
    public GameObject attackIcon;
    public HealthBar enemyHealthBar;
    public HealthBar playerHealthBar;
    public Text playerHealthText;
    public Text enemyHealthText;
    public Text actionText;
    bool enemyAtking;
    public VariableCheck varCheck;


    public void Start()
    {
        playerMaxHealth = 25 + varCheck.upgMH;
        enemyHealth = 1;
        playerHealth = playerMaxHealth;
        actionText.GetComponent<Text>().enabled = false;
        enemyAtking = true;
        attackIcon.SetActive(false);
        healthIcon.SetActive(false);
        randVar = Random.Range(0, 2);
        enemyHealthText.text = "HP: " + enemyHealth.ToString() + " / " + enemyMaxHealth.ToString();
        playerHealthText.text = "HP: " + playerHealth.ToString() + " / " + playerMaxHealth.ToString();
    }


    public void PlayerAttacks() // Player turn of Attack
    {
        
        if (playerHealth > 0)
        {
            playerPower = 5 + varCheck.upgAtk;
            enemyPower = Random.Range(4, 6);
            enemyHealth -= playerPower;
            enemyHealthBar.SetHealth(playerHealth);
            enemyHealthText.text = "HP: " + enemyHealth.ToString() + " / " + enemyMaxHealth.ToString();
            if(enemyHealth < 0)
            {
                enemyHealth = 0;
            }
            EnemyTurn();
        }
    }
    public void PlayerHeals() //Player turn of Healing
    {
        if (playerHealth > 0)
        {
            playerPower = 5 + varCheck.upgAtk;
            enemyPower = Random.Range(4, 6);
            playerHealth += (5 + varCheck.upgHeal);
            playerHealthBar.SetHealth(playerHealth);
            playerHealthText.text = "HP: " + playerHealth.ToString() + " / " + playerMaxHealth.ToString();
            EnemyTurn();
        }
    }

    public void EnemyTurn() //Enemy Turn
    {
        actionText.GetComponent<Text>().enabled = true;

        if (enemyHealth > 0) 
        {
            if(randVar == 1)
            {
                playerHealth -= enemyPower;
                randVar = Random.Range(0, 2);
                playerHealthText.text = "HP: " + playerHealth.ToString() + " / " + playerMaxHealth.ToString();
                enemyAtking = true;
                Debug.Log("Enemy Attacked");
            }
            else
            {
                enemyHealth += 3;
                randVar = Random.Range(0, 2);
                enemyHealthText.text = "HP: " + enemyHealth.ToString() + " / " + enemyMaxHealth.ToString();
                enemyAtking = false;
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

        if (enemyAtking == true)
        {
            actionText.text = "Enemy Attacked for " + enemyPower;
        }
        else
        {
            actionText.text = "Enemy Healed for 5";
        }

        if (enemyHealth < 0)
        {
            enemyHealth = 0;
        }

        if (playerHealth <= 0)
        {
            print("Game Lost!");
            actionText.text = "Game Lost";
            Debug.Log("Enemy Health: " + enemyHealth);
            this.enabled = false;
        }
        else if (enemyHealth <= 0)
        {
            print("Game Won!");
            actionText.text = "Game Won";
            Debug.Log("Player Health: " + playerHealth);
            SceneManager.LoadScene("Upgrades");
            this.enabled = false;
            varCheck.sceneNum++;
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

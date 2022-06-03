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
    int playerPower;
    public int randVar;
    public GameObject iconHealth;
    public GameObject iconAttack;
    public HealthBar enemyHealthBar;
    public HealthBar playerHealthBar;
    public Text playerHealthText;
    public Text enemyHealthText;
    public Text actionText;
    public Text levelText;
    bool enemyAtking;
    private VariableCheck varCheck;


    public void Start()
    {
        varCheck = GameObject.Find("Variables").GetComponent<VariableCheck>(); //Establishes Connection with Variables Script
        // Initialising Variables
        playerMaxHealth = 20 + varCheck.upgMH;
        if (varCheck.sceneNum % 5 == 0)
        {
            enemyMaxHealth = (int)(1.5 * varCheck.enemyMaxHP);
        }
        else
        {
            enemyMaxHealth = varCheck.enemyMaxHP;
        }
        playerHealth = playerMaxHealth;
        enemyHealth = enemyMaxHealth;
        // Enables the on-screen visuals
        actionText.GetComponent<Text>().enabled = false;
        enemyAtking = true;
        iconAttack.SetActive(false);
        iconHealth.SetActive(false);
        randVar = Random.Range(1, 6);
        enemyHealthText.text = "HP: " + enemyHealth.ToString() + " / " + enemyMaxHealth.ToString();
        playerHealthText.text = "HP: " + playerHealth.ToString() + " / " + playerMaxHealth.ToString();
        levelText.text = "Level " + varCheck.sceneNum.ToString();
        // Sets Max Health of Health Bar
        enemyHealthBar.SetMaxHealth(enemyMaxHealth);
        playerHealthBar.SetMaxHealth(playerMaxHealth);
        EnableButtons();
    }


    public void PlayerAttacks() // Player turn of Attack
    {
        
        if (playerHealth > 0)
        {
            playerPower = 5 + varCheck.upgAtk;
            enemyHealth -= playerPower;
            enemyHealthBar.SetHealth(enemyHealth);
            enemyHealthText.text = "HP: " + enemyHealth.ToString() + " / " + enemyMaxHealth.ToString();
            if(enemyHealth < 0)
            {
                enemyHealth = 0;
            }
            actionText.text = "Player Attacked for " + (playerPower);
            Debug.Log("Player Attacked for " + (playerPower));
            if (enemyHealth > 0)
            {
                EnemyTurn();
            }
        }
    }
    public void PlayerHeals() //Player turn of Healing
    {
        if (playerHealth > 0)
        {
            playerPower = 5 + varCheck.upgAtk;
            playerHealth += (5 + varCheck.upgHeal);
            if (playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }
            playerHealthBar.SetHealth(playerHealth);
            playerHealthText.text = "HP: " + playerHealth.ToString() + " / " + playerMaxHealth.ToString();
            actionText.text = "Player Healed for " + (5 + varCheck.upgHeal);
            Debug.Log("Player Healed for " + (5 + varCheck.upgHeal));
            if (enemyHealth > 0)
            {
                EnemyTurn();
            }
            
        }
    }

    public void EnemyTurn() //Enemy Turn
    {
        actionText.GetComponent<Text>().enabled = true;

        if (enemyAtking == false)
        {
            EnemyHeals();
        }
        else if(enemyAtking == true)
        {
            EnemyAttacks();
        }

        enemyHealthBar.SetHealth(enemyHealth);
        playerHealthBar.SetHealth(playerHealth);
    }

    public void EnemyAttacks()
    {
        playerHealth -= varCheck.enemyAtk;
        
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }
        playerHealthText.text = "HP: " + playerHealth.ToString() + " / " + playerMaxHealth.ToString();
        actionText.text += "\nEnemy Attacked for " + varCheck.enemyAtk.ToString();
        randVar = Random.Range(1, 6);
        EnableButtons();
        Debug.Log("Enemy Attacked");
    }

    public void EnemyHeals()
    {
        enemyHealth += 5;
        enemyHealthText.text = "HP: " + enemyHealth.ToString() + " / " + enemyMaxHealth.ToString();
        actionText.text += "\nEnemy Healed for 5";
        randVar = Random.Range(1, 6);
        EnableButtons();
        Debug.Log("Enemy Healed");
    }

    public void EnableButtons()
    {
        if (playerHealth >= playerMaxHealth) //Disables the Heal Button if player is on full health
        {
            GameObject.Find("Defend").GetComponent<Button>().interactable = false;
            GameObject.Find("Attack").GetComponent<Button>().interactable = true;
        }
        else
        {
            GameObject.Find("Defend").GetComponent<Button>().interactable = true;
            GameObject.Find("Attack").GetComponent<Button>().interactable = true;
        }
        
    }


    public void Update() //Constantly Checking
    {
        if (randVar >= 4 && enemyHealth <= (int)(.25 * enemyMaxHealth)) //Changes enemy icon, depending on what their next move is
        {
            iconAttack.SetActive(false);
            iconHealth.SetActive(true);
            enemyAtking = false;
        }
        else
        {
            iconAttack.SetActive(true);
            iconHealth.SetActive(false);
            enemyAtking = true;
        }

        if (playerHealth <= 0) // Lose Condition
        {
            print("Game Lost!");
            actionText.text = "Game Lost at Level " + varCheck.sceneNum.ToString();
            SceneManager.LoadScene("GameOver");
            Debug.Log("Enemy Health: " + enemyHealth);
            this.enabled = false;
        }

        if (enemyHealth <= 0) // Win Condition
        {
            print("Fight Won!");
            actionText.text = "Fight Won";
            Debug.Log("Player Health: " + playerHealth);
            SceneManager.LoadScene("Upgrades");
            this.enabled = false;
            if (varCheck.sceneNum % 2 == 0)
            {
                varCheck.enemyMaxHP += 5;
            }
            if (varCheck.sceneNum % 4 == 0)
            {
                varCheck.enemyAtk += 2;
            }
            varCheck.sceneNum++;
        }

        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }

        if (enemyHealth >= enemyMaxHealth)
        {
            randVar = 1;
        }
    }
}

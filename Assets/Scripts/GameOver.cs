using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private VariableCheck varCheck;
    public Text levelText;

    public void Start()
    {
        varCheck = GameObject.Find("Variables").GetComponent<VariableCheck>();
        levelText.text = "You Lost on Level " + varCheck.sceneNum;
        ResetVariables();
    }

    private void ResetVariables()
    {
        varCheck.upgMH = 0;
        varCheck.upgHeal = 0;
        varCheck.upgAtk = 0;
        varCheck.sceneNum = 1;
        varCheck.enemyMaxHP = 20;
        varCheck.enemyAtk = 5;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Battle");
        Debug.Log("Resetting Game");

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }

}

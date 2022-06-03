using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    
    public VariableCheck varCheck;

    public void MaxHealth()
    {
        varCheck = GameObject.FindWithTag("Variable").GetComponent<VariableCheck>();
        varCheck.upgMH += 5;
        SceneManager.LoadScene(varCheck.sceneNum);
        Debug.Log("Loaded Scene: " + varCheck.sceneNum);
    }

    public void Healing()
    {
        varCheck.upgHeal += 3;
        SceneManager.LoadScene(varCheck.sceneNum);
    }

    public void Attack()
    {
        varCheck.upgAtk += 2;
        SceneManager.LoadScene(varCheck.sceneNum);
    }
}

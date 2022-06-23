using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int playerMaxHP;
    public int playerHeal;
    public int playerAtk;
    public int level;
    public int enemyMaxHP;
    public int enemyAtk;

    public PlayerData (VariableCheck player)
    {
        level = player.sceneNum;

        playerMaxHP = player.upgMH;
        playerAtk = player.upgAtk;
        playerHeal = player.upgHeal;

        enemyMaxHP = player.enemyMaxHP;
        enemyAtk = player.enemyAtk;
    }
}

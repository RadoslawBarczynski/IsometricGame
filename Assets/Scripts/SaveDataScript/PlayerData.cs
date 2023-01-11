using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public long money;
    public int level;
    public string[] cardnames;
    //public List<GameObject> deckInUse;

    public PlayerData(GameManager gameManager)
    {
        money = gameManager.money;
        level = gameManager.level;
        cardnames = gameManager.cardnames;
        //deckInUse = gameManager.deckInUse;
    }
}

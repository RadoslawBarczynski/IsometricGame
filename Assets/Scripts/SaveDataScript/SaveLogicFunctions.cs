using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLogicFunctions : MonoBehaviour
{
    public GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void SavePlayer()
    {
        SaveSystemScript.SavePlayer(gameManager);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystemScript.LoadPlayer();

        gameManager.money = data.money;
        gameManager.level = data.level;
        gameManager.cardnames = data.cardnames;
        //gameManager.deckInUse = data.deckInUse;
    }

    public void SaveDeckToGameManager()
    {
        string[] tempStrings = new string[gameManager.cardnamesList.Count];
        for (int i = 0; i < gameManager.cardnamesList.Count; i++)
        {
            tempStrings[i] += gameManager.cardnamesList[i];
        }
        gameManager.cardnames = tempStrings;
    }
}

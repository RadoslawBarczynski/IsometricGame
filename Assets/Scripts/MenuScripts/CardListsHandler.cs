using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class is checking if cardnames value from saved data is > then cardlist and if yes, function has to full the second one 
public class CardListsHandler : MonoBehaviour
{
    public GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.cardnames.Length > gameManager.cardnamesList.Count)
        {
            for (int i = 0; i < gameManager.cardnames.Length; i++)
            {
                gameManager.cardnamesList.Add(gameManager.cardnames[i]);
            }
        }
    }
}

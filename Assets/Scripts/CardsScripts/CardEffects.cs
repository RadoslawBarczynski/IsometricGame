using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects : MonoBehaviour
{
    public Card card;
    public GameLogic gameLogic; //for player movement card
    public PlayerStats playerStats;
    public CharacterInfo characterInfo;
    private MapManager mapManager;
    public GameObject BombPrefab;
    public static bool isArrowUsed;

    private void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        characterInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInfo>();
        mapManager = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapManager>();
    }
    public void Use()
    {
        if (card.id == 1) //MoveCard
        {
            gameLogic.isPlayerMoveActive = true;
        }
        else if(card.id == 2) //ManaRefreshCard
        {
            playerStats.mana = playerStats.maxMana;
        }
        else if (card.id == 3) //HealCard
        {
            playerStats.HealPlayer(3);
        }
        else if (card.id == 4) //pickACard
        {

        }
        else if (card.id == 5) //armor
        {
            Debug.Log("CardUsed");
            playerStats.GrantArmor(1);
        }
        else if (card.id == 6) //bomb
        {
            SetBomb();
        }
        else if (card.id == 7) //arrow
        {
            isArrowUsed = true;
            Debug.Log("Arrow");
        }
        else if (card.id == 8) 
        {
            Debug.Log("Function 4");
        }
        playerStats.TakeMana(card.manaCost);
        Destroy(this.gameObject);
    }

    public void IsEnoughMana(Draggable d)
    {
        if(playerStats.mana >= card.manaCost)
        {
            Use();
            Destroy(d.placeholder);
        }
        else
        {
            Debug.Log("Not Enough Mana");
        }
    }

    public void SetBomb()
    {
        GameLogic.isBombActive = true;
        Vector3 vector = new Vector3(mapManager.tilesList[characterInfo.PlayerTileIndex].transform.position.x, mapManager.tilesList[characterInfo.PlayerTileIndex].transform.position.y + 0.1f, mapManager.tilesList[characterInfo.PlayerTileIndex].transform.position.z);
        GameObject clone = (GameObject)Instantiate(BombPrefab, vector, Quaternion.identity);
    }

   


}

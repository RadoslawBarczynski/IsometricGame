using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public OverlayTile bombTile;
    public GameManager gameManager;
    public GameLogic gameLogic;
    public CharacterInfo characterInfo;
    public MapManager mapManager;
    private MissionScript missionScript;
    public PlayerStats playerStats;
    public GameObject player;
    public int index;
    public float timeleft;
    public List<int> creaturesInRange = new List<int>();
    public List<GameObject> toDestroy = new List<GameObject>();



    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameLogic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        characterInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInfo>();
        mapManager = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapManager>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        missionScript = GameObject.FindWithTag("GameLogic").GetComponent<MissionScript>();
        player = GameObject.FindWithTag("Player");
        SetIndex();
    }


    public void ExplodeBomb()
    {
        DetectEnemies();
        if (toDestroy.Count > 0)
            missionScript.CompleteMission(2);
        foreach(GameObject go in toDestroy)
        {
            gameLogic.EnemysList.Remove(go);
            gameLogic.Creatures[go.GetComponent<EnemyAI>().enemyNubmerOfTurn] = 80;
            go.GetComponent<EnemyAI>().DeathPopUp();
            RepairTurns(go.GetComponent<EnemyAI>().enemyNubmerOfTurn);
        }
        foreach (GameObject gm in toDestroy)
        {
            gameLogic.EnemyKilled++;
            Destroy(gm);                        
        }
        if (gameLogic.EnemysList.Count == 0)
        {
            missionScript.CompleteMission(0);
            gameManager.money += playerStats.gold;
            gameLogic.endingPanel.SetActive(true);
        }
        GameLogic.isBombActive = false;
        Debug.Log("Exploasion!");
        Destroy(this.gameObject);
    }

    public void SetIndex()
    {
        index = characterInfo.PlayerTileIndex;
        bombTile = characterInfo.standingOnTile;
    }

    public void FindCreature(int indexValue)
    {
        List<GameObject> list = gameLogic.EnemysList;
        foreach(GameObject creature in list)
        {
            if(indexValue == creature.GetComponent<EnemyAI>().enemyTileStandIndex)
            {
                toDestroy.Add(creature);
                break;
            }
        }
        if ((indexValue == characterInfo.PlayerTileIndex && GameLogic.isBombActive == true) || (characterInfo.PlayerTileIndex == index && GameLogic.isBombActive == true))
        {
            playerStats.TakeDamage(3);
        }
    }

    public void RepairTurns(int turnNum)
    {
        List<GameObject> listx = gameLogic.EnemysList;
        Debug.Log(turnNum + "wprowadzone");
        foreach (GameObject creaturex in listx)
        {
            if (turnNum < creaturex.GetComponent<EnemyAI>().enemyNubmerOfTurn)
            {
                Debug.Log(creaturex.GetComponent<EnemyAI>().enemyNubmerOfTurn + " przed");
                creaturex.GetComponent<EnemyAI>().enemyNubmerOfTurn--;
                Debug.Log(creaturex.GetComponent<EnemyAI>().enemyNubmerOfTurn + " po");
            }
        }
    }

    public void DetectEnemies()
    {
        int tile = index;
        if (tile + 1 < 64 && tile + 1 > -1)
        {
            if (!gameLogic.borders.ContainsKey(tile + 1) && gameLogic.Creatures.ContainsValue(tile + 1))
            {
                FindCreature(tile + 1);
            }
        }
        if (tile - 1 < 64 && tile - 1 > -1)
        {
            if (!gameLogic.borders2.ContainsKey(tile - 1) && gameLogic.Creatures.ContainsValue(tile - 1))
            {
                FindCreature(tile - 1);
            }
        }
        if (tile + 8 < 64 && tile + 8 > -1)
        {
            if (gameLogic.Creatures.ContainsValue(tile + 8))
            {
                FindCreature(tile + 8);

            }
        }
        if (tile - 8 < 64 && tile - 8 > -1)
        {
            if (gameLogic.Creatures.ContainsValue(tile - 8))
            {
                FindCreature(tile - 8);

            }
        }
        if (tile + 7 < 64 && tile + 7 > -1)
        {
            if (!gameLogic.borders.ContainsKey(tile + 7) && gameLogic.Creatures.ContainsValue(tile + 7))
            {
                FindCreature(tile + 7);
            }
        }
        if (tile - 7 < 64 && tile - 7 > -1)
        {
            if (!gameLogic.borders2.ContainsKey(tile - 7) && gameLogic.Creatures.ContainsValue(tile - 7) )
            {
                FindCreature(tile - 7);
            }
        }
        if (tile - 9 < 64 && tile - 9 > -1)
        {
            if (!gameLogic.borders2.ContainsKey(tile - 9) && gameLogic.Creatures.ContainsValue(tile - 9))
            {
                FindCreature(tile -9);
            }
        }
        if (tile + 9 < 64 && tile + 9 > -1)
        {
            if (!gameLogic.borders.ContainsKey(tile + 9) && gameLogic.Creatures.ContainsValue(tile + 9))
            {
                FindCreature(tile + 9);
            }
        }
        FindCreature(tile);
    }
}

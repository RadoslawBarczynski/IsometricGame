using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyAI : MonoBehaviour
{
    //other scripts
    private MapManager mapManager;
    private GameLogic gameLogic;
    private PlayerStats playerStats;
    private CharacterInfo characterInfo;
    private MissionScript missionScript;
    private GameManager gameManager;
    //tilelogic
    public OverlayTile tileParent; //actual tile
    public int enemyTileStandIndex;
    public bool isEnemyTurn;
    public int enemyNubmerOfTurn;
    private bool isPlayerInRange;
    //statsvariables
    public int Damage = 1;
    public int Health = 3;
    public float speed = 6f;
    public bool isEnemyMoving = false;
    public float timeLeft;
    public bool attackedOnce = false;
    //prefabs
    public GameObject EnemyTextPopUpPrefab;
    public GameObject hitEffect;

    //movetiles
    OverlayTile tileUp;
    OverlayTile tileDown;
    OverlayTile tileLeft;
    OverlayTile tileRight;

    //[SerializeField]
    //public List<int> detectionTiles = new List<int>();

    void Start()
    {
        mapManager = GameObject.FindWithTag("MapController").GetComponent<MapManager>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        characterInfo = GameObject.FindWithTag("Player").GetComponent<CharacterInfo>();
        gameLogic = GameObject.FindWithTag("GameLogic").GetComponent<GameLogic>();
       missionScript = GameObject.FindWithTag("GameLogic").GetComponent<MissionScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameLogic.turnCounter == enemyNubmerOfTurn && isEnemyTurn == true)
        {
            DetectPlayer();
            if (isEnemyTurn == true)
            {
                //gameLogic.Creatures[1] = characterInfo.PlayerTileIndex;
                gameLogic.isPlayerMoveActive = false;
                //Debug.Log("EnemyTurn with index: " + enemyTileStandIndex);
                gameLogic.Creatures[enemyNubmerOfTurn] = enemyTileStandIndex;
                //DetectPlayer();
                if (isPlayerInRange == true)
                {                    
                    AttackPlayer(1);
                    isPlayerInRange = false;
                }
                else
                {
                    MoveEnemy();                    
                    isEnemyTurn = false;
                    gameLogic.Creatures[enemyNubmerOfTurn] = enemyTileStandIndex;
                    gameLogic.TurnIncrement();
                }
            }
        }       
        if (isEnemyMoving == true)
        {
            MoveAlongPath(tileParent);
        }

    }

    public void setStats(int tileIndex, int turnNubmer)
    {
        //enemyTileStand = mapManager.tilesList[tileIndex].GetComponent<OverlayTile>();
        enemyNubmerOfTurn = turnNubmer;
        enemyTileStandIndex = tileIndex;
        Debug.Log(enemyNubmerOfTurn);
    }

    public void AttackPlayer(int damage)
    {
        isEnemyTurn = false;
        //Debug.Log("-" + damage);
        playerStats.TakeDamage(damage);
        playerStats.DecreaseGold(damage + 5);
        gameLogic.TurnIncrement();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Vector2 pos = this.gameObject.transform.position;
        Quaternion rotation = this.gameObject.transform.rotation;
        var go = Instantiate(EnemyTextPopUpPrefab, pos, rotation);
        var gi = Instantiate(hitEffect, pos, rotation);
        go.GetComponentInChildren<TextMeshPro>().text =  "-" + damage;
        if (Health <= 0)
        {
            if (gameLogic.EnemysList.Count == 1)
            {
                missionScript.CompleteMission(0);
                gameManager.money += playerStats.gold;
                gameLogic.endingPanel.SetActive(true);
            }
            DeathPopUp();
            Die();
        }
    }

    public void Die()
    {
        gameLogic.EnemysList.Remove(this.gameObject);
        gameLogic.Creatures[this.gameObject.GetComponent<EnemyAI>().enemyNubmerOfTurn] = 80;
        foreach (GameObject creaturex in gameLogic.EnemysList)
        {
            if (enemyNubmerOfTurn < creaturex.GetComponent<EnemyAI>().enemyNubmerOfTurn)
            {
                creaturex.GetComponent<EnemyAI>().enemyNubmerOfTurn--;
            }
        }
        var go = Instantiate(EnemyTextPopUpPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponentInChildren<TextMeshPro>().text = "Died!";
        gameLogic.EnemyKilled++;
        Destroy(this.gameObject, 0.1f);
    }

    public void MoveAlongPath(OverlayTile rememberedTile)
    {
        var step = speed * Time.deltaTime;

        tileParent = rememberedTile;
        Vector2 fixedTilePosition = new Vector2(tileParent.transform.position.x, tileParent.transform.position.y + 0.15f);

        var zIndex = tileParent.transform.position.z;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, fixedTilePosition, step);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, zIndex);
        if (Vector2.Distance(gameObject.transform.position, tileParent.transform.position) < 0.35f)
        {
            gameObject.transform.position = new Vector3(tileParent.transform.position.x, tileParent.transform.position.y + 0.3f, tileParent.transform.position.z);
            isEnemyMoving = false;
        }
    }

    public void MoveEnemy()
    {
        List<OverlayTile> movingposibiliies = new List<OverlayTile>();
        int tile = enemyTileStandIndex;
            if (tile + 1 < 64 && tile + 1 > -1)
            {
                if (!gameLogic.borders.ContainsKey(tile + 1) && !gameLogic.Creatures.ContainsValue(tile + 1) && !gameLogic.InteractableIndexes.Contains(tile + 1))
                {
                    tileUp = mapManager.tilesList[tile + 1].GetComponent<OverlayTile>();
                    movingposibiliies.Add(tileUp);
                    
                }
            }
            if (tile - 1 < 64 && tile - 1 > -1)
            {
                if (!gameLogic.borders2.ContainsKey(tile - 1) && !gameLogic.Creatures.ContainsValue(tile - 1) && !gameLogic.InteractableIndexes.Contains(tile-1))
                {
                    tileDown = mapManager.tilesList[tile - 1].GetComponent<OverlayTile>();
                    movingposibiliies.Add(tileDown);
                
                }
            }
            if (tile + 8 < 64 && tile + 8 > -1)
            {
                if (!gameLogic.Creatures.ContainsValue(tile + 8) && !gameLogic.InteractableIndexes.Contains(tile + 8))
                {
                    tileRight = mapManager.tilesList[tile + 8].GetComponent<OverlayTile>();
                    movingposibiliies.Add(tileRight);
                
                }
            }
            if (tile - 8 < 64 && tile - 8 > -1)
            {
                if (!gameLogic.Creatures.ContainsValue(tile - 8) && !gameLogic.InteractableIndexes.Contains(tile - 8))
                {
                    tileLeft = mapManager.tilesList[tile - 8].GetComponent<OverlayTile>();
                    movingposibiliies.Add(tileLeft);
                
                }
            }

            int j = Random.Range(0, movingposibiliies.Count - 1);

        if (movingposibiliies.Count > 0)
        {
            tileParent = movingposibiliies[j];
            enemyTileStandIndex = mapManager.tilesList.IndexOf(tileParent.gameObject);
            isEnemyMoving = true;
        }

    }

    public void DetectPlayer()
    {
        List<int> detectionTiles = new List<int>();
        int tile = enemyTileStandIndex;
        int tempUp = tile + 1;
        int tempDown = tile - 1;
        int tempRight = tile + 8;
        int tempLeft = tile - 8;
        if (tile + 1 < 64 && tile + 1 > -1)
        {
            if (!gameLogic.borders.ContainsKey(tile + 1))
            {
                detectionTiles.Add(tile +1);
            }
        }
        if (tile - 1 < 64 && tile - 1 > -1)
        {
            if (!gameLogic.borders2.ContainsKey(tile + 1))
            {
                detectionTiles.Add(tile - 1);
            }
        }
        if (tile + 8 < 64 && tile +8 > -1)
        {
                detectionTiles.Add(tile + 8);
        }
        if (tile - 8 < 64 && tile - 8 > -1)
        {
                detectionTiles.Add(tile - 8);
        }

        if (detectionTiles.Count > 0)
        {
            foreach (int tileses in detectionTiles)
            {
                if (tileses == characterInfo.PlayerTileIndex)
                {
                    isPlayerInRange = true;
                    return;
                }
                else
                {
                    isPlayerInRange = false;                    
                }
            }
            return;
        }
          
    }

    public void TurnStatus()
    {
        if(GameLogic.turnCounter == enemyNubmerOfTurn)
        {
            isEnemyMoving = true;
        }
    }

    public void DeathPopUp()
    {
        Vector2 pos = this.gameObject.transform.position;
        Quaternion rotation = this.gameObject.transform.rotation;
        var go = Instantiate(EnemyTextPopUpPrefab, pos, rotation);
        var gi = Instantiate(hitEffect, pos, rotation);
        go.GetComponentInChildren<TextMeshPro>().text = "Died!";
    }
}

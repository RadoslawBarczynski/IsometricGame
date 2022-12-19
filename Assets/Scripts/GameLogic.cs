using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GameLogic : MonoBehaviour
{
    //OTHER SCRIPTS
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private MapManager mapManager;
    [SerializeField]
    public MovingScript movingScript;
    public CharacterInfo character;
    public UiLogic uiLogic;
    public BombScript Bomb;
    //PREFABS
    public GameObject treasureElement;
    public GameObject enemyPrefab;
    public GameObject mapElement;
    public GameObject TextPopUpPrefab;
    //VARIABLES
    public int treasuresNumber;
    public int interactableCount;
    public int counter = 0;
    public int enemyNumber;
    public int EnemyKilled = 0;
    [SerializeField]
    public bool isPlayerMoveActive;
    //STATIC VARIABLES
    public static float timeLeft = 0;
    public static int turnCounter = 1;
    public static bool isBombActive;
    //UI ELEMENTS
    [SerializeField]
    private TextMeshProUGUI counterText;
    public GameObject endingPanel;


    //movetiles
    OverlayTile tileUp;
    OverlayTile tileDown;
    OverlayTile tileLeft;
    OverlayTile tileRight;

    //EnemyVariables
    public List<Interactable> interactables = new List<Interactable>();
    public List<GameObject> EnemysList = new List<GameObject>();
    public List<int> EnemyStartIndexes = new List<int>();
    public List<int> InteractableIndexes = new List<int>();
    public List<int> treasureIndexes = new List<int>();
    public List<GameObject> interactablesList = new List<GameObject>();

    //dictionaries to avoid other site blocks highlight
    public Dictionary<int, string> borders = new Dictionary<int, string>
    { //blocks for tile + 1
        {0, "avoid1" },
        {8, "avoid2" },
        {16, "avoid3" },
        {24, "avoid4" },
        {32, "avoid5" },
        {40, "avoid6" },
        {48, "avoid7" },
        {56, "avoid8" }
    };

    public Dictionary<int, string> borders2 = new Dictionary<int, string>
    {   //blocks for tile - 1
        {7, "avoid1" },
        {15, "avoid2" },
        {23, "avoid3" },
        {31, "avoid4" },
        {39, "avoid5" },
        {47, "avoid6" },
        {55, "avoid7" },
        {63, "avoid8" }
    };
    public Dictionary<int, int> Creatures = new Dictionary<int, int>
    { //blocks for creatures
        {1, 100},
        {2, 70 },
        {3, 80 },
        {4, 80 },
        {5, 80 },
        {6, 80 }
    };

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        character = player.GetComponent<CharacterInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = "Turn: " + turnCounter + "/" + (EnemysList.Count + 1);
        if (mapManager.isMapReneringEnded == true)
        {
            if (counter == 0)
            {
                SetPlayerSpawn();
                turnCounter++; //set next enemy turn
                counter++;
            } else if (counter < enemyNumber+1)
            {
                for (int i = 0; i < enemyNumber; i++)
                {                   
                    GetRandomTile(turnCounter);
                    turnCounter++;                   
                }
                CreateMapObject();
            }
            else if (counter == enemyNumber + 1)
            {
                mapManager.isMapReneringEnded = false;
                turnCounter = 1; //reset turn to one 
            }           
        }


        if (isPlayerMoveActive == true && movingScript.isMovingAlready == false && turnCounter == 1)
        {
            PlayerMoveDisplay();
        }
        //end round timer
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0.2f && turnCounter < EnemysList.Count + 1)
            {
                turnCounter++;
                timeLeft = -1;
            }
            else if(timeLeft < 0.2f && turnCounter == EnemysList.Count + 1)
            {
                player.GetComponent<PlayerStats>().GetMana(player.GetComponent<PlayerStats>().maxMana);
                uiLogic.SetButtonBack();
                if(isBombActive == true)
                {
                    FindAnyBomb();
                }
                turnCounter = 1;
                timeLeft = -1;
            }
        }
    }

    public void GetRandomTile(int turn) //enemy Respawn
    {
        if (EnemyStartIndexes.Count == 0)
        {
            int i = Random.Range(1, 31);
            Vector3 vector = new Vector3(mapManager.tilesList[i].transform.position.x, mapManager.tilesList[i].transform.position.y + 0.3f, mapManager.tilesList[i].transform.position.z);
            GameObject clone = (GameObject)Instantiate(enemyPrefab, vector, Quaternion.identity);
            EnemyAI enemyAi = clone.GetComponent<EnemyAI>();
            EnemysList.Add(clone);
            enemyAi.setStats(i, turn);
            EnemyStartIndexes.Add(i);
            counter++;
        }else
        {
            int i = Random.Range(1, 31);
            while (EnemyStartIndexes.Contains(i))
            {
                i = Random.Range(1, 31);
            }
            Vector3 vector = new Vector3(mapManager.tilesList[i].transform.position.x, mapManager.tilesList[i].transform.position.y + 0.3f, mapManager.tilesList[i].transform.position.z);
            GameObject clone = (GameObject)Instantiate(enemyPrefab, vector, Quaternion.identity);
            EnemyAI enemyAi = clone.GetComponent<EnemyAI>();
            EnemysList.Add(clone);
            enemyAi.setStats(i, turn);
            EnemyStartIndexes.Add(i);
            counter++;
        }
    }

    public void CreateMapObject()
    {
        for (int j = 0; j < interactableCount - treasuresNumber; j++)
        {
            int i = Random.Range(1, 41);
            while (EnemyStartIndexes.Contains(i))
            {
                i = Random.Range(1, 41);
            }
            Vector3 vectorI = new Vector3(mapManager.tilesList[i].transform.position.x, mapManager.tilesList[i].transform.position.y + 0.1f, mapManager.tilesList[i].transform.position.z);
            GameObject interactableObject = (GameObject)Instantiate(mapElement, vectorI, Quaternion.identity);
            //mapManager.tilesList[i].GetComponent<OverlayTile>().isTileInteractable = true;
            EnemyStartIndexes.Add(i);
            InteractableIndexes.Add(i);
        }
        for (int x = 0; x < treasuresNumber; x++)
        {
            int i = Random.Range(1, 41);
            while (EnemyStartIndexes.Contains(i))
            {
                i = Random.Range(1, 41);
            }
            Vector3 vectorII = new Vector3(mapManager.tilesList[i].transform.position.x, mapManager.tilesList[i].transform.position.y + 0.1f, mapManager.tilesList[i].transform.position.z);
            GameObject treasureeObject = (GameObject)Instantiate(treasureElement, vectorII, Quaternion.identity);
            mapManager.tilesList[i].GetComponent<OverlayTile>().isTileInteractable = true;
            treasureeObject.GetComponent<InteractableDisplay>().setInteractableTile(mapManager.tilesList[i].GetComponent<OverlayTile>());
            treasureeObject.GetComponent<InteractableDisplay>().indexo = i;
            EnemyStartIndexes.Add(i);
            treasureIndexes.Add(i);
            InteractableIndexes.Add(i);
            interactablesList.Add(treasureeObject);
        }
    }

    //moving is based on matrix 1 to 64
    //   | 9 | 6 | 3 |
    //   | 8 | 5 | 2 |
    //   | 7 | 4 | 1 |
    // index + 1 means go up, index - 1 means go down
    // index + 3 means go left, index - 3 means go right
    public void PlayerMoveDisplay()
    {
        int tile = mapManager.tilesList.IndexOf(character.standingOnTile.gameObject);
        if (tile + 1 < 64 && tile + 1 > -1)
        {
            if (!borders.ContainsKey(tile + 1) && !Creatures.ContainsValue(tile+1) && !InteractableIndexes.Contains(tile + 1))
            {
                tileUp = mapManager.tilesList[tile + 1].GetComponent<OverlayTile>();
                tileUp.ShowTile();
            }
        }
        if (tile - 1 < 64 && tile - 1 > -1)
        {
            if (!borders2.ContainsKey(tile - 1) && !Creatures.ContainsValue(tile - 1) && !InteractableIndexes.Contains(tile - 1))
            {
                tileDown = mapManager.tilesList[tile - 1].GetComponent<OverlayTile>();
                tileDown.ShowTile();
            }
        }
        if (tile + 8 < 64 && tile + 8 > -1) {
            if (!Creatures.ContainsValue(tile + 8) && !InteractableIndexes.Contains(tile + 8))
            {
                tileRight = mapManager.tilesList[tile + 8].GetComponent<OverlayTile>();
                tileRight.ShowTile();
            }
        }
        if (tile - 8 < 64 && tile - 8 > -1) {
            if (!Creatures.ContainsValue(tile - 8) && !InteractableIndexes.Contains(tile - 8))
            {
                tileLeft = mapManager.tilesList[tile - 8].GetComponent<OverlayTile>();
                tileLeft.ShowTile();
            }
        }
    }


    public void HideTiles()
    {
        if(tileUp.isTileActive == true)
        {
            tileUp.HideTile();
        }
        if(tileDown.isTileActive == true)
        {
            tileDown.HideTile();
        }
        if(tileRight.isTileActive == true)
        {
            tileRight.HideTile();
        }
        if(tileLeft.isTileActive == true)
        {
            tileLeft.HideTile();
        }
    }

    public void SetPlayerSpawn() //player Spawn
    {
        int i = Random.Range(49, 54);
        character.transform.position = new Vector3(mapManager.tilesList[i].transform.position.x, mapManager.tilesList[i].transform.position.y + 0.3f, mapManager.tilesList[i].transform.position.z);
        character.standingOnTile = mapManager.tilesList[i].GetComponent<OverlayTile>();
        character.PlayerTileIndex = i;
        Creatures[1] = character.PlayerTileIndex;
    }

    public void TurnIncrement()
    {
        if (turnCounter == 1)
        {
            foreach (GameObject enemy in EnemysList)
            {
                enemy.GetComponent<EnemyAI>().isEnemyTurn = true;
            }            
        }

        timeLeft = 3f;
        
    }

    public void FindAnyBomb()
    {
        Bomb = GameObject.FindGameObjectWithTag("Bomb").GetComponent<BombScript>();
        Bomb.ExplodeBomb();
    }


}

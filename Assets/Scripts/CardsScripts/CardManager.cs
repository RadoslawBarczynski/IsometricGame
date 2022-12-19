using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private DropZone dropZone;
    [SerializeField]
    private List<GameObject> cardsInDeck = new List<GameObject>();
    [SerializeField]
    private List<GameObject> CardPrefabs;
    public GameManager gameManager;
    public GameLogic gameLogic;
    private int HandSize = 4;

    public int CardsInDeckValue;
    public GameObject cardPrefab;
    public GameObject hand;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameLogic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        CollectDeck();
        //GenerateRandomDeck();
        Fisher_Yates_CardDeck_Shuffle(cardsInDeck);
        CardsInDeckValue = cardsInDeck.Count;
        for (int i = 0; hand.transform.childCount < HandSize; i++)
        {
            GetCard();
        }

    }

    void Update()
    {
        RoundCheck();
    }

    public void GetCard()
    {
        if (hand.transform.childCount < HandSize)
        {
            for (int i = 0; hand.transform.childCount < HandSize; i++)
            {
                var newCard = Instantiate(cardsInDeck[CardsInDeckValue - 1]);
                newCard.transform.parent = hand.transform;
                cardsInDeck.RemoveAt(CardsInDeckValue - 1);
                CardsInDeckValue--;
            }
        }
    }

    public void RoundCheck()
    {
        if (GameLogic.turnCounter == 1 && GameLogic.timeLeft <= 0)
        {
            dropZone.enabled = true;
        }
        else
        {
            dropZone.enabled = false;
        }
    }

    public void GenerateRandomDeck()
    {
        for (int i = 0; i < 30; i++)
        {           
            if(i <= 13)
            {
                cardsInDeck.Add(CardPrefabs[0]);
            }
            else if(i > 13 && i < 21)
            {
                int j = Random.Range(1, 5);
                cardsInDeck.Add(CardPrefabs[j]);
            }
            else
            {
                int j = Random.Range(5, 7);
                cardsInDeck.Add(CardPrefabs[j]);
            }
        }
    }

    public void CollectDeck()
    {
        foreach(GameObject cardf in gameManager.deckInUse)
        {
            cardsInDeck.Add(cardf);
        }
    }

    public static List<GameObject> Fisher_Yates_CardDeck_Shuffle(List<GameObject> alist)
    {
        System.Random _random = new System.Random();

        GameObject myGO;

        int n = alist.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = alist[r];
            alist[r] = alist[i];
            alist[i] = myGO;
        }

        return alist;
    }

    public void FindEnemyToShot(OverlayTile tile)
    {
        foreach (GameObject enemy in gameLogic.EnemysList)
        {
            if (enemy.GetComponent<EnemyAI>().tileParent == tile)
            {
                enemy.GetComponent<EnemyAI>().TakeDamage(2);
                CardEffects.isArrowUsed = false;
                return;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefabsInCity : MonoBehaviour
{
    public List<GameObject> cardsInShop = new List<GameObject>();
    public List<GameObject> OriginalCardsPrefabs;
    public List<GameObject> tempDeck;
    public GameManager gameManager;

    //UI elements
    public Button deleteButton;
    public GameObject cardSlot;
    public GameObject cardSlotLoad;
    public GameObject cardSlotParent;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();        
        foreach(GameObject cardObject in cardsInShop)
        {
            Instantiate(cardObject, transform);
        }
        LoadDeckState();
    }
    public void Update()
    {
        if(cardSlotParent.transform.childCount == 0)
        {
            deleteButton.interactable = false;
        }
        else
        {
            deleteButton.interactable = true;
        }
    }


    public void ChooseCard(int value) 
    {
        if(value == 1) //move
        {
            tempDeck.Add(OriginalCardsPrefabs[value - 1]);
            addCardSlot(tempDeck[tempDeck.Count-1]);
        }
        else if(value == 2) //refreshmana
        {
            tempDeck.Add(OriginalCardsPrefabs[value - 1]);
            addCardSlot(tempDeck[tempDeck.Count - 1]);
        }
        else if(value == 3) //armor
        {
            tempDeck.Add(OriginalCardsPrefabs[value - 1]);
            addCardSlot(tempDeck[tempDeck.Count - 1]);
        }
        else if(value == 4) //heal
        {
            tempDeck.Add(OriginalCardsPrefabs[value - 1]);
            addCardSlot(tempDeck[tempDeck.Count - 1]);
        }
        else if (value == 5) //bomb
        {
            tempDeck.Add(OriginalCardsPrefabs[value - 1]);
            addCardSlot(tempDeck[tempDeck.Count - 1]);
        }
        else if (value == 6) //arrow
        {
            tempDeck.Add(OriginalCardsPrefabs[value - 1]);
            addCardSlot(tempDeck[tempDeck.Count-1]);
        }
    }

    public void SaveDeck()
    {
        gameManager.deckInUse.Clear();
        foreach(GameObject cards in tempDeck)
        {
            gameManager.deckInUse.Add(cards);
        }
    }

    public void addCardSlot(GameObject cardPrefab)
    {
        Instantiate(cardSlot, cardSlotParent.transform);
    }
    public void addCardSlotLoad(string name)
    {
        this.cardSlotLoad.GetComponent<CarsSlotLoaddisplay>().SetName(name);
        Instantiate(cardSlotLoad, cardSlotParent.transform);
    }

    public void DestroyAllSlots()
    {
        for(var i = cardSlotParent.transform.childCount - 1; i >= 0 ; i--)
        {
            Object.Destroy(cardSlotParent.transform.GetChild(i).gameObject);
        }
    }

    public void DeleteCard()
    {
        tempDeck.Remove(tempDeck[tempDeck.Count - 1]);
        gameManager.cardnamesList.Remove(gameManager.cardnamesList[gameManager.cardnamesList.Count - 1]);
        gameManager.cardnames = gameManager.cardnames.Take(gameManager.cardnames.Length - 1).ToArray();
        Destroy(cardSlotParent.transform.GetChild(cardSlotParent.transform.childCount - 1).gameObject);
    }

    public void LoadDeckState()
    {
        for (int i = 0; i < gameManager.cardnames.Length; i++)
        {         
            if (gameManager.cardnames[i] == "Move")
            {
                tempDeck.Add(OriginalCardsPrefabs[1 - 1]);
                addCardSlotLoad(gameManager.cardnames[i]);
            }
            else if(gameManager.cardnames[i] == "Mana Crystal")
            {
                tempDeck.Add(OriginalCardsPrefabs[2 - 1]);
                addCardSlotLoad(gameManager.cardnames[i]);
            }
            else if (gameManager.cardnames[i] == "Fortify")
            {
                tempDeck.Add(OriginalCardsPrefabs[3 - 1]);
                addCardSlotLoad(gameManager.cardnames[i]);
            }
            else if (gameManager.cardnames[i] == "Potion")
            {
                tempDeck.Add(OriginalCardsPrefabs[3 - 1]);
                addCardSlotLoad(gameManager.cardnames[i]);
            }
            else if (gameManager.cardnames[i] == "Bomb")
            {
                tempDeck.Add(OriginalCardsPrefabs[3 - 1]);
                addCardSlotLoad(gameManager.cardnames[i]);
            }
            else if (gameManager.cardnames[i] == "Arrow")
            {
                tempDeck.Add(OriginalCardsPrefabs[3 - 1]);
                addCardSlotLoad(gameManager.cardnames[i]);
            }
        }
    }
}

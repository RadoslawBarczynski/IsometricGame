using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPrefabsInCity : MonoBehaviour
{
    public List<GameObject> cardsInShop = new List<GameObject>();
    public List<GameObject> OriginalCardsPrefabs;
    public List<GameObject> tempDeck;
    public GameManager gameManager;

    public GameObject cardSlot;
    public GameObject cardSlotParent;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        foreach(GameObject cardObject in cardsInShop)
        {
            Instantiate(cardObject, transform);
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
        foreach(GameObject cards in tempDeck)
        {
            gameManager.deckInUse.Add(cards);
        }
        tempDeck.Clear();
    }

    public void addCardSlot(GameObject cardPrefab)
    {
        Instantiate(cardSlot, cardSlotParent.transform);
    }

    public void DestroyAllSlots()
    {
        for(var i = cardSlotParent.transform.childCount - 1; i >= 0 ; i--)
        {
            Object.Destroy(cardSlotParent.transform.GetChild(i).gameObject);
        }
    }
}

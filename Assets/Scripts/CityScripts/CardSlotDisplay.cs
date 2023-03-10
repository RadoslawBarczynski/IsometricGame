using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSlotDisplay : MonoBehaviour
{
    public TextMeshProUGUI cardname;
    public CardPrefabsInCity cardPrefabsInCity;
    public GameManager gameManager;

    public void Start()
    {
        cardPrefabsInCity = GameObject.Find("CardsSelection").GetComponent<CardPrefabsInCity>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cardname.text = cardPrefabsInCity.tempDeck[cardPrefabsInCity.tempDeck.Count - 1].GetComponent<CardDisplay>().card.name;
        gameManager.cardnamesList.Add(cardname.text);
    }
}

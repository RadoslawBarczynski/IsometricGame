using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSlotDisplay : MonoBehaviour
{
    public TextMeshProUGUI cardname;
    public CardPrefabsInCity cardPrefabsInCity;

    public void Start()
    {
        cardPrefabsInCity = GameObject.Find("CardsSelection").GetComponent<CardPrefabsInCity>();
        cardname.text = cardPrefabsInCity.tempDeck[cardPrefabsInCity.tempDeck.Count - 1].GetComponent<CardDisplay>().card.name;
    }
}

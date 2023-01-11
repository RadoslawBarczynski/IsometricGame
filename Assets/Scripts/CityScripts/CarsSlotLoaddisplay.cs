using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarsSlotLoaddisplay : MonoBehaviour
{
    public TextMeshProUGUI cardname;
    public CardPrefabsInCity cardPrefabsInCity;
    public GameManager gameManager;

    public void Start()
    {
        cardPrefabsInCity = GameObject.Find("CardsSelection").GetComponent<CardPrefabsInCity>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetName(string name)
    {
        cardname.text = name;
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}

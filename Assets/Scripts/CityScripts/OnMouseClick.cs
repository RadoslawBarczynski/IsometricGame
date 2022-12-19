using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseClick : MonoBehaviour, IPointerClickHandler
{
    public CardPrefabsInCity cardPrefabsInCity;
    public int value;
    private void Start()
    {
        cardPrefabsInCity = GameObject.Find("CardsSelection").GetComponent<CardPrefabsInCity>();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        cardPrefabsInCity.ChooseCard(value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    public GameObject CardSelectionPanel;
    public CardPrefabsInCity cardPrefabsInCity;

    public void PanelSelection(int value)
    {
        if (value == 0)
        {
            cardPrefabsInCity.tempDeck.Clear();
            cardPrefabsInCity.DestroyAllSlots();
            CardSelectionPanel.SetActive(false);
        }
        else if(value == 1)
        {
            CardSelectionPanel.SetActive(true);
        }
    }

}

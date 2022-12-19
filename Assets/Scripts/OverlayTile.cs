using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    public bool isTileActive;
    public bool isTileInteractable;
    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            HideTile();
        }*/
    }

    public void interactableOn()
    {
        isTileInteractable = true;
    }

    public void interactableOff()
    {
        isTileInteractable = false;
    }

    public void ShowTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        isTileActive = true;
    }

    public void HideTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        isTileActive = false;
    }

}


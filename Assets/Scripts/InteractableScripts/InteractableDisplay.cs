using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractableDisplay : MonoBehaviour
{
    public Interactable interactable;
    public GameLogic gameLogic;
    public int indexo;

    public OverlayTile InteractableTile;
    public SpriteRenderer sprite;
    public PlayerStats playerStats;

    void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        RandomTreasure(gameLogic.interactables);        
    }

    public void RandomTreasure(List<Interactable> interactables)
    {
        int i = Random.Range(0, 3);
        interactable = interactables[i];
        sprite.sprite = interactable.artObject;
    }

    public void setInteractableTile(OverlayTile tile)
    {
        InteractableTile = tile;
    }

    public void AwakeTHis()
    {
        playerStats.GetGold(interactable.gold);
        Debug.Log("Its " + interactable.name);
        InteractableTile.interactableOff();
        InteractableTile = null;
        gameLogic.treasureIndexes.Remove(indexo);
        gameLogic.interactablesList.Remove(this.gameObject);
        gameLogic.InteractableIndexes.Remove(indexo);
        Destroy(this.gameObject);
    }


}

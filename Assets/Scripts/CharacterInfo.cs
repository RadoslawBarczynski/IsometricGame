using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

public class CharacterInfo : MonoBehaviour
{
    public MapManager mapManager;
    public GameLogic gameLogic;
    public OverlayTile standingOnTile;
    public OverlayTile interactTileRange;
    public MissionScript missionScript;
    public int PlayerTileIndex;
    public bool isInteractableInRange;

    public List<GameObject> treasuresInRange = new List<GameObject>();

    private void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        missionScript = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<MissionScript>();
        mapManager = GameObject.Find("Grid").GetComponent<MapManager>();
    }
    private void Update()
    {
        if (standingOnTile != null)
            DetectInteractable();
    }

    //¿eby móc tworzyæ interakcje z wiêcej ni¿ jednym obiektem to dodawaæ do listy i potem ich szukaæ
    public void DetectInteractable()
    {
        int tile = mapManager.tilesList.IndexOf(standingOnTile.gameObject);
        if (tile + 1 < 64 && tile + 1 > -1)
        {
            if (gameLogic.treasureIndexes.Contains(tile + 1) && !gameLogic.borders.ContainsKey(tile + 1))
            {
                interactTileRange = mapManager.tilesList[tile + 1].GetComponent<OverlayTile>();
                isInteractableInRange = true;
                return;
            }
            else
            {
                interactTileRange = null;
                isInteractableInRange = false;
            }
        }
        if (tile - 1 < 64 && tile - 1 > -1)
        {
            if (gameLogic.treasureIndexes.Contains(tile - 1) && !gameLogic.borders.ContainsKey(tile - 1) && isInteractableInRange == false)
            {
                interactTileRange = mapManager.tilesList[tile - 1].GetComponent<OverlayTile>();
                isInteractableInRange = true;
                return;
            }
            else
            {
                interactTileRange = null;
                isInteractableInRange = false;
            }
        }
        if (tile + 8 < 64 && tile + 8 > -1)
        {
            if (gameLogic.treasureIndexes.Contains(tile + 8) && isInteractableInRange == false)
            {
                interactTileRange = mapManager.tilesList[tile + 8].GetComponent<OverlayTile>();
                isInteractableInRange = true;
                return;
            }
            else
            {
                interactTileRange = null;
                isInteractableInRange = false;
            }
        }
        if (tile - 8 < 64 && tile - 8 > -1)
        {
            if (gameLogic.treasureIndexes.Contains(tile - 8) && isInteractableInRange == false)
            {
                interactTileRange = mapManager.tilesList[tile - 8].GetComponent<OverlayTile>();
                isInteractableInRange = true;
                return;
            }
            else
            {
                interactTileRange = null;
                isInteractableInRange = false;
            }
        }
    }


    public void FindInteractable(List<GameObject> list, OverlayTile tile)
    {
        
        foreach (GameObject interactable in list)
        {
            if (interactable.GetComponent<InteractableDisplay>().InteractableTile == tile)
            {
                treasuresInRange.Remove(interactable);
                interactable.GetComponent<InteractableDisplay>().AwakeTHis();
                missionScript.CheckInteractablesAmount();
                break;
            }
        }
    }
}

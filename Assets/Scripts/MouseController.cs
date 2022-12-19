using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private CharacterInfo character;
    private OverlayTile currentPlayerTile;
    [SerializeField]
    private GameLogic gameLogic;
    [SerializeField]
    private MapManager mapManager;
    public float speed;
    public bool isMovingAlready;
    [SerializeField]
    private MovingScript movingScript;
    [SerializeField]
    private CardManager cardManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        var focusedTileHit = GetFocusedOnTile();
        

        if (focusedTileHit.HasValue)
        {
            OverlayTile overlayTile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();
            transform.position = overlayTile.transform.position;
            //gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

            if (Input.GetMouseButtonDown(0))
            {
                //overlayTile.GetComponent<OverlayTile>().ShowTile();
                if (gameLogic.isPlayerMoveActive == true && overlayTile.isTileActive == true)
                {
                    movingScript.RememberTile(overlayTile);
                    movingScript.isMovingAlready = true;
                    gameLogic.isPlayerMoveActive = false;
                    gameLogic.HideTiles();
                    //MoveAlongPath(overlayTile);
                    //PositionCharacterOnTile(overlayTile);
                }
                else if (character.isInteractableInRange == true && overlayTile.isTileInteractable == true &&overlayTile == character.interactTileRange)
                {
                    character.FindInteractable(gameLogic.interactablesList, overlayTile);
                }
                else if(CardEffects.isArrowUsed == true)
                {
                    cardManager.FindEnemyToShot(overlayTile);
                }
            }
        }
    }


    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        if(hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }
        return null;
    }

    public void PositionCharacterOnTile(OverlayTile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.3f, tile.transform.position.z);
        //character.activeTile - tile;
        character.standingOnTile = tile;
        character.PlayerTileIndex = mapManager.tilesList.IndexOf(tile.gameObject);
        gameLogic.Creatures[1] = character.PlayerTileIndex;
    }
}

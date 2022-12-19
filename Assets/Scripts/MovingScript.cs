using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MovingScript : MonoBehaviour
{
    [SerializeField]
    private CharacterInfo character;
    [SerializeField]
    private MouseController mouseController;
    [SerializeField]
    GameLogic gameLogic;
    private OverlayTile tile;
    public float speed;
    public bool isMovingAlready = false;

    // Update is called once per frame
    void Update()
    {
        if(isMovingAlready == true)
        {
            MoveAlongPath(tile);
        }
    }

    public void MoveAlongPath(OverlayTile rememberedTile)
    {
        var step = speed * Time.deltaTime;

        tile = rememberedTile;
        Vector2 fixedTilePosition = new Vector2(tile.transform.position.x, tile.transform.position.y+0.15f);

        var zIndex = tile.transform.position.z;
        character.transform.position = Vector2.MoveTowards(character.transform.position, fixedTilePosition, step);
        character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, zIndex);
        if (Vector2.Distance(character.transform.position, tile.transform.position) < 0.35f)
        {
            mouseController.PositionCharacterOnTile(tile);
            isMovingAlready = false;
        }
    }

    public void RememberTile(OverlayTile rememberedTile)
    {
        tile = rememberedTile;
    }
}

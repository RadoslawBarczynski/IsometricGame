using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiLogic : MonoBehaviour
{
    public Button TurnButton;
    public Sprite spriteLocked;
    public Sprite spriteUnlocked;
    public TextMeshProUGUI EnemyCounter;
    public GameLogic gameLogic;
    public List<GameObject> armorPieces;
    public PlayerStats playerStats;
    void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        SetArmorUi();
        EnemyCounter.text = gameLogic.EnemyKilled.ToString();
    }

    public void ChangeButton()
    {
        TurnButton.image.sprite = spriteLocked;
        TurnButton.interactable = false;
    }

    public void SetButtonBack()
    {
        TurnButton.image.sprite = spriteUnlocked;
        TurnButton.interactable = true;
    }

    public void GoBackButton()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void SetArmorUi()
    {
        if(playerStats.armor == 0)
        {
            armorPieces[0].SetActive(false);
        }
        else if(playerStats.armor == 1)
        {
            armorPieces[0].SetActive(true);
            armorPieces[1].SetActive(false);
        }
        else if(playerStats.armor == 2)
        {
            armorPieces[1].SetActive(true);
            armorPieces[2].SetActive(false);
        }
        else if(playerStats.armor == 3)
        {
            armorPieces[2].SetActive(true);
        }
        
    }
}

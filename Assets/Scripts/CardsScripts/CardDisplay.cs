using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public Image cardBackground;
    public Image artwork;

    public TextMeshProUGUI manaText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;

        cardBackground.sprite = card.CardBackground;
        artwork.sprite = card.artwork;

        manaText.text = card.manaCost.ToString();
    }

}

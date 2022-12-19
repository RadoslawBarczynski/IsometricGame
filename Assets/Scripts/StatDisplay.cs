using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ManaText;
    [SerializeField]
    private TextMeshProUGUI HealtText;
    [SerializeField]
    private TextMeshProUGUI GoldValue;

    public Slider HealthSlider;
    public Slider ManaSlider;

    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        HealtText.text = playerStats.health + "/" + playerStats.maxHealth;
        ManaText.text = playerStats.mana + "/" + playerStats.maxMana;
        GoldValue.text = playerStats.gold+ "g";
    }

    public void SetMaxHealth(int health)
    {
        HealthSlider.maxValue = health;
    }

    public void SetHealth(int health)
    {
        HealthSlider.value = health;
    }
    public void SetMaxMana(int mana)
    {
        ManaSlider.maxValue = mana;
    }

    public void SetMana(int mana)
    {
        ManaSlider.value = mana;
    }
}

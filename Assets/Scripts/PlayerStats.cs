using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int maxHealth = 5;
    public int mana;
    public int maxMana = 3;
    public int armor = 0;
    public int gold = 0;
    public GameObject DmgPopUp;
    public GameObject hitEffect;

    public StatDisplay statDisplay;

    private void Start()
    {
        health = maxHealth;
        mana = maxMana;
        statDisplay.SetMaxMana(maxMana);
        statDisplay.SetMaxHealth(maxHealth);
        statDisplay.SetHealth(maxHealth);
        statDisplay.SetMana(maxMana);
    }

    public void TakeDamage(int damage)
    {
        if (armor > 0)
        {
            damage = 1;
            armor--;
            var go = Instantiate(DmgPopUp, transform.position, Quaternion.identity, transform);
            go.GetComponentInChildren<TextMeshPro>().text = "-" + damage + " armor!";
            var gi = Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        }
        else if(armor == 0)
        {
            var go = Instantiate(DmgPopUp, transform.position, Quaternion.identity, transform);
            go.GetComponentInChildren<TextMeshPro>().text = "-" + damage;
            var gi = Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
            health -= damage;
        }
        statDisplay.SetHealth(health);
    }

    public void TakeMana(int manaValue)
    {
        mana -= manaValue;
        statDisplay.SetMana(mana);
    }

    public void HealPlayer(int healthValue)
    {
        health += healthValue;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        statDisplay.SetHealth(health);
    }

    public void GetMana(int manaValue)
    {
        mana += manaValue;
        if(mana > maxMana)
        {
            mana = maxMana;
        }
        statDisplay.SetMana(mana);
    }

    public void GetGold(int goldValue)
    {
        gold += goldValue;
    }

    public void DecreaseGold(int goldValue)
    {
        gold -= goldValue;
        if (gold < 0)
            gold = 0;
    }

    public void GrantArmor(int Armorvalue)
    {
        armor += Armorvalue;
    }
}

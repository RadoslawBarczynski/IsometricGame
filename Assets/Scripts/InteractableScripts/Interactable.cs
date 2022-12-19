using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interactable", menuName = "interactable")]
public class Interactable : ScriptableObject
{
    public new string name;
    public int gold;

    public Sprite artObject;

    public bool needToUnlock;

    public virtual void Use()
    {
        Debug.Log("Used " + name);
    }
}

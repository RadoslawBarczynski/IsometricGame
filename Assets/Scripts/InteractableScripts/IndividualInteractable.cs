using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interactable", menuName = "interactable/Test")]
public class IndividualInteractable : Interactable
{
    public override void Use()
    {
        Debug.Log("DiffrenUse");
    }
}

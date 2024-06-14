using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/PlaceholderItem")]
public class PlaceholderItem : Item
{
    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("ADDED ITEM!");
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        Debug.Log("STACKED ITEM!");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MetalPipe")]
public class MetalPipe : Item
{
    public override void Initialize()
    {
        base.Initialize();
        itemStats.distanceDamageModifier += 1.5f;
        itemStats.minDistance += 2;
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        itemStats.distanceDamageModifier += 1.5f;
        itemStats.minDistance += 2;
    }
}
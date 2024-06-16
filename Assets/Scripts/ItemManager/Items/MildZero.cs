using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MildZero")]
public class MildZero : Item
{
    public override void Initialize()
    {
        base.Initialize();
        itemStats.fireRateModifier -= 0.15f;
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        itemStats.movementSpeedModifier -= 0.15f;
    }
}
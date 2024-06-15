using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/FullestAuto")]
public class FullestAuto : Item
{
    public override void Initialize()
    {
        base.Initialize();
        itemStats.ammoDamageModifier += 2;
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        itemStats.ammoDamageModifier += 2;
    }
}
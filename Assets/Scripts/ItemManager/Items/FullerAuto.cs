using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/FullerAuto")]
public class FullerAuto : Item
{
    public override void Initialize()
    {
        base.Initialize();
        itemStats.magSizeModifier += 0.15f;
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        itemStats.magSizeModifier += 0.15f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/WD69")]
public class WD69 : Item
{
    public override void Initialize()
    {
        base.Initialize();
        itemStats.movementSpeedModifier += 0.1f;
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        itemStats.movementSpeedModifier += 0.1f;
    }
}

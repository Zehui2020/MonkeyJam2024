using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/SafetyHelmet")]
public class SafetyHelmet : Item
{
    public override void Initialize()
    {
        base.Initialize();
        itemStats.critHealChance += 10;
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        itemStats.critHealChance += 10;
    }
}
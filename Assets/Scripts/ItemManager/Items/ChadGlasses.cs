using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ChadGlasses")]
public class ChadGlasses : Item
{
    public override void Initialize()
    {
        base.Initialize();
        itemStats.critRate += 10;
        itemStats.critDamage += 0.05f;
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        itemStats.critRate += 10;
        itemStats.critDamage += 0.05f;
    }
}
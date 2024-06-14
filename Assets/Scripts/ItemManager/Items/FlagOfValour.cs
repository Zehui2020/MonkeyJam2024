using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/FlagOfValour")]
public class FlagOfValour : Item
{
    public override void Initialize()
    {
        base.Initialize();
        itemStats.stunRadius += 2;
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        itemStats.stunRadius += 2;
    }
}
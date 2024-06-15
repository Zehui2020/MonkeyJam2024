using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/GambaLoad")]
public class GambaLoad : Item
{
    public override void Initialize()
    {
        base.Initialize();
        itemStats.gambaReloadChance += 8;
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        itemStats.gambaReloadChance += 8;
    }
}
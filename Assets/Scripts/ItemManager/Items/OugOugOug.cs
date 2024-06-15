using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/OugOugOug")]
public class OugOugOug : Item
{
    public override void Initialize()
    {
        base.Initialize();
        itemStats.rocketBananaAmount += 5;
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        itemStats.rocketBananaAmount += 5;
    }
}
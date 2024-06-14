using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/DroneShot")]
public class DroneShot : Item
{
    public override void Initialize()
    {
        base.Initialize();
        ItemManager.Instance.SpawnDrone();
    }

    public override void IncrementStack()
    {
        base.IncrementStack();
        ItemManager.Instance.UpgradeDrone();
    }
}
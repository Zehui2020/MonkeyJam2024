using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStats")]
public class ItemStats : ScriptableObject
{
    public float movementSpeedModifier;
    public float fireRateModifier;
    public int critRate;
    public float critDamage;
    public int critHealChance;
    public int rocketBananaAmount;
    public int gambaReloadChance;
    public float distanceDamageModifier;
    public int minDistance;
    public int stunRadius;
    public int ammoDamageModifier;

    public void ResetStats()
    {
        movementSpeedModifier = 1f;
        fireRateModifier = 1f;
        critRate = 5;
        critDamage = 2;
        critHealChance = 0;
        rocketBananaAmount = 0;
        gambaReloadChance = 0;
        distanceDamageModifier = 1;
        minDistance = 3;
        stunRadius = 0;
        ammoDamageModifier = 0;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTable")]
public class ItemTable : ScriptableObject
{
    public List<Item> itemTable = new List<Item>();
    public List<Item> weaponTable = new List<Item>();

    [NonSerialized]
    public int totalWeight = -1;

    public void ResetStacks()
    {
        foreach (Item item in itemTable)
            item.itemStack = 0;

        foreach (Item item in weaponTable)
            item.itemStack = 0;
    }

    private void CalculateTotalWeight(List<Item> table)
    {
        totalWeight = 0;
        for (int i = 0; i < table.Count; i++)
            totalWeight += (int)table[i].itemRarity;
    }

    private Item GetNewItem(List<Item> itemTable)
    {
        CalculateTotalWeight(itemTable);
        int roll = UnityEngine.Random.Range(0, totalWeight);

        for (int i = 0; i < itemTable.Count; i++)
        {
            roll -= (int)itemTable[i].itemRarity;

            if (roll < 0)
                return itemTable[i];
        }

        return itemTable[0];
    }

    public List<Item> GetUniqueItems(int count)
    {
        List<Item> itemTableCopy = new List<Item>();
        itemTableCopy.AddRange(itemTable);

        List<Item> uniqueItems = new List<Item>();

        if (count > itemTable.Count)
            count = itemTable.Count;

        for (int i = 0; i < count; i++)
        {
            Item item = GetNewItem(itemTableCopy);
            itemTableCopy.Remove(item);
            uniqueItems.Add(item);
        }

        return uniqueItems;
    }

    public List<Item> GetUniqueWeapons(int count)
    {
        List<Item> weaponTableCopy = new List<Item>();
        weaponTableCopy.AddRange(weaponTable);

        List<Item> uniqueWeapons = new List<Item>();

        if (count > weaponTable.Count)
            count = weaponTable.Count;

        for (int i = 0; i < count; i++)
        {
            Item item = GetNewItem(weaponTableCopy);
            weaponTableCopy.Remove(item);
            uniqueWeapons.Add(item);
        }

        return uniqueWeapons;
    }
}
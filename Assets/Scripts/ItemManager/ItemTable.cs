using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTable")]
public class ItemTable : ScriptableObject
{
    public List<Item> itemTable = new List<Item>();

    [NonSerialized]
    public int totalWeight = -1;

    private void CalculateTotalWeight()
    {
        totalWeight = 0;
        for (int i = 0; i < itemTable.Count; i++)
            totalWeight += (int)itemTable[i].itemRarity;
    }

    private Item GetNewItem()
    {
        CalculateTotalWeight();
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
            Item item = GetNewItem();
            itemTableCopy.Remove(item);
            uniqueItems.Add(item);
        }

        return uniqueItems;
    }
}
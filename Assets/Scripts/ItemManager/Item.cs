using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : ScriptableObject
{
    public ItemStats itemStats;

    public enum Rarity { Common = 18 , Uncommon = 10, Legendary = 1 };
    public Rarity itemRarity;

    public enum ItemType {  };
    public ItemType itemType;

    public Sprite spriteIcon;
    [TextArea(3, 10)]
    public string title;
    [TextArea(3, 10)]
    public string description;
    public int itemStack;

    public virtual void Initialize() { }

    public virtual void IncrementStack() { itemStack++; }

    public virtual void DecrementStack() { itemStack--; }

    public void SetCount(int newCount) { itemStack = newCount; }

    public string GetTitle() 
    {
        switch (itemRarity)
        {
            case Rarity.Common:
                return title;
            case Rarity.Uncommon:
                return "<color=#4AFF2E>" + title + "</color>";
            case Rarity.Legendary:
                return "<color=#FF0F0F>" + title + "</color>";
        }

        return string.Empty;
    }

    public virtual string GetDescription() { return description; }
}
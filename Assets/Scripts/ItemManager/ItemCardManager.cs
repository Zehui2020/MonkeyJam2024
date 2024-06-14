using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardManager : MonoBehaviour
{
    [SerializeField] private ItemTable itemTable;
    [SerializeField] private List<ItemCard> itemCards = new List<ItemCard>();

    public void SetupCards()
    {
        List<Item> items = itemTable.GetUniqueItems(itemCards.Count);

        for (int i = 0; i < itemCards.Count; i++)
            itemCards[i].SetupCard(items[i]);
    }

    private void OnEnable()
    {
        SetupCards();
    }
}
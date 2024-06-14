using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardManager : MonoBehaviour
{
    [SerializeField] private ItemTable itemTable;
    [SerializeField] private List<ItemCard> itemCards = new List<ItemCard>();

    private Item chosenItem;

    public void SetupCards()
    {
        List<Item> items = itemTable.GetUniqueItems(itemCards.Count);

        for (int i = 0; i < itemCards.Count; i++)
        {
            itemCards[i].SetupCard(items[i]);
            itemCards[i].OnSelectEvent += SetChosenItem;
        }
    }

    public void SetChosenItem(Item item)
    {
        chosenItem = item;
    }

    public void ConfirmItem()
    {
        ItemManager.Instance.AddItem(chosenItem);
        chosenItem = null;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SetupCards();
    }
}
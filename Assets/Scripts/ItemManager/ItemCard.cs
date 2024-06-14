using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCard : MonoBehaviour
{
    private Item cardItem;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public event System.Action<Item> OnSelectEvent;

    public void SetupCard(Item item)
    {
        cardItem = item;
        itemImage.sprite = item.spriteIcon;
        itemName.text = item.title;
        itemDescription.text = item.description;
    }

    public void SelectCard()
    {
        OnSelectEvent?.Invoke(cardItem);
    }
}
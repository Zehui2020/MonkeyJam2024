using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public void SetupCard(Item item)
    {
        itemImage.sprite = item.spriteIcon;
        itemName.text = item.title;
        itemDescription.text = item.description;
    }
}
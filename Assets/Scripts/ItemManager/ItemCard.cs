using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCard : MonoBehaviour
{
    public Item cardItem;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Animator cardAnimator;

    public event System.Action<ItemCard> OnSelectEvent;

    public void SetupCard(Item item)
    {
        cardItem = item;
        itemImage.sprite = item.spriteIcon;
        itemName.text = item.title;
        itemDescription.text = item.description;
    }

    public void SetSelectCardAnimation(bool select)
    {
        cardAnimator.SetBool("click", select);
    }

    public void OnConfirmCard()
    {
        cardAnimator.SetTrigger("confirm");
    }

    public void SelectCard()
    {
        SetSelectCardAnimation(true);
        OnSelectEvent?.Invoke(this);
    }
}
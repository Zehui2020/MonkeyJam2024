using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCardManager : MonoBehaviour
{
    [SerializeField] private ItemTable itemTable;
    [SerializeField] private List<ItemCard> itemCards = new List<ItemCard>();
    [SerializeField] private TextMeshProUGUI header;

    private ItemCard chosenCard;

    public void SetupTutorialWeaponPickup(Item weaponItem)
    {
        header.text = "Select Your Weapon";

        itemCards[1].SetupCard(weaponItem);
        itemCards[1].OnSelectEvent += SetChosenItem;

        itemCards[0].gameObject.SetActive(false);
        itemCards[2].gameObject.SetActive(false);
    }

    public void SetupItemCards()
    {
        List<Item> items = itemTable.GetUniqueItems(itemCards.Count);

        for (int i = 0; i < itemCards.Count; i++)
        {
            itemCards[i].gameObject.SetActive(true);
            itemCards[i].SetupCard(items[i]);
            itemCards[i].OnSelectEvent += SetChosenItem;
        }

        header.text = "Select Your Item";
    }

    public void SetupWeaponCards()
    {
        List<Item> items = itemTable.GetUniqueWeapons(itemCards.Count);

        for (int i = 0; i < itemCards.Count; i++)
        {
            itemCards[i].gameObject.SetActive(true);
            itemCards[i].SetupCard(items[i]);
            itemCards[i].OnSelectEvent += SetChosenItem;
        }

        header.text = "Select Your Weapon";
    }

    public void SetChosenItem(ItemCard card)
    {
        chosenCard = card;
        foreach (ItemCard itemCard in itemCards)
        {
            if (card.Equals(itemCard))
                continue;

            itemCard.SetSelectCardAnimation(false);
        }
    }

    public void ConfirmItem()
    {
        StartCoroutine(OnConfirm());
    }

    private IEnumerator OnConfirm()
    {
        chosenCard.OnConfirmCard();
        ItemManager.Instance.AddItem(chosenCard.cardItem);
        Debug.Log("CALLED");

        yield return new WaitForSeconds(1f);

        chosenCard = null;
        gameObject.SetActive(false);
    }
}
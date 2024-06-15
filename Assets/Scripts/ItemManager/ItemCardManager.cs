using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardManager : MonoBehaviour
{
    [SerializeField] private ItemTable itemTable;
    [SerializeField] private List<ItemCard> itemCards = new List<ItemCard>();

    private ItemCard chosenCard;

    public void SetupCards()
    {
        List<Item> items = itemTable.GetUniqueItems(itemCards.Count);

        for (int i = 0; i < itemCards.Count; i++)
        {
            itemCards[i].SetupCard(items[i]);
            itemCards[i].OnSelectEvent += SetChosenItem;
        }
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

        yield return new WaitForSeconds(1f);

        chosenCard = null;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SetupCards();
    }
}
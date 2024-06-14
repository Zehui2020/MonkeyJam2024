using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private List<ItemPickup> itemPickups = new List<ItemPickup>();

    [SerializeField] private int cost;
    [SerializeField] private GameObject costUI;
    [SerializeField] private TextMeshProUGUI costText;

    public void InitInteractable()
    {
        costText.text = cost.ToString();
        costUI.SetActive(false);
    }

    public void OnEnterRange()
    {
        costUI.SetActive(true);
    }

    public void OnExitRange()
    {
        costUI.SetActive(false);
    }

    public void OnInteract()
    {
        int randNum = Random.Range(0, itemPickups.Count);
        Instantiate(itemPickups[randNum], transform.position, Quaternion.identity);
    }

    public void SetCost(int newCost)
    {
        cost = newCost;
        costText.text = cost.ToString();
    }
}
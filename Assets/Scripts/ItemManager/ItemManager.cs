using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public List<Item> itemList;

    [SerializeField] private Drone dronePrefab;
    private Drone drone;
    [SerializeField] private Transform droneFollowPos;

    [SerializeField] private ItemCardManager itemCardManager;

    public void InitItemManager()
    {
        itemList = new List<Item>();
        Instance = this;
    }

    public void AddItem(Item itemToAdd)
    {
        if (FindItemInList(itemToAdd) == null)
        {
            itemToAdd.Initialize();
            itemList.Add(itemToAdd);
        }
        else
        {
            itemToAdd.IncrementStack();
        }
    }

    public void DecreaseStack(Item itemToRemove)
    {
        if (FindItemInList(itemToRemove) != null)
        {
            itemToRemove.DecrementStack();
            CheckItemStack(itemToRemove);
        }
    }

    public Item FindItemByName(Item.ItemType type)
    {
        foreach (Item item in itemList)
        {
            if (item.itemType == type)
                return item;
        }

        return null;
    }

    private Item FindItemInList(Item itemToFind)
    {
        foreach (Item item in itemList)
        {
            if (item.itemType == itemToFind.itemType)
                return item;
        }

        return null;
    }

    private void CheckItemStack(Item itemToCheck)
    {
        if (itemToCheck.itemStack <= 0)
            itemList.Remove(itemToCheck);
    }

    public void SpawnDrone()
    {
        drone = Instantiate(dronePrefab, transform.position, Quaternion.identity);
        Debug.Log(drone);
        drone?.SetupDrone(droneFollowPos);
    }

    public void UpgradeDrone()
    {
        drone?.UpgradeDrone();
    }

    public void OpenTutorialCardChoices(Item weapon)
    {
        Time.timeScale = 0;
        itemCardManager.gameObject.SetActive(true);
        itemCardManager.SetupTutorialWeaponPickup(weapon);
    }


    public void OpenItemCardChoices()
    {
        Time.timeScale = 0;
        itemCardManager.gameObject.SetActive(true);
        itemCardManager.SetupItemCards();
    }

    public void OpenWeaponCardChoices()
    {
        Time.timeScale = 0;
        itemCardManager.gameObject.SetActive(true);
        itemCardManager.SetupWeaponCards();
    }
}
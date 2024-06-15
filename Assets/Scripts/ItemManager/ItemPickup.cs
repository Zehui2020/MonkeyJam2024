using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemPickupType
    { 
        WEAPON,
        ITEM
    }
    public ItemPickupType itemPickupType;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (itemPickupType == ItemPickupType.ITEM)
                ItemManager.Instance.OpenItemCardChoices();

            else if (itemPickupType == ItemPickupType.WEAPON)
                ItemManager.Instance.OpenWeaponCardChoices();

            Destroy(gameObject);
        }
    }
}
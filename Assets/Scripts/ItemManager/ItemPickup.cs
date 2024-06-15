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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            if (itemPickupType == ItemPickupType.ITEM)
                ItemManager.Instance.OpenItemCardChoices();

            Destroy(gameObject);
        }
    }
}
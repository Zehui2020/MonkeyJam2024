using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private EntityAudioController entityAudioController;
    public enum ItemPickupType
    { 
        WEAPON,
        ITEM
    }
    public ItemPickupType itemPickupType;

    private void Start()
    {
        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            entityAudioController.PlayAudio("pickup");
            if (itemPickupType == ItemPickupType.ITEM)
                ItemManager.Instance.OpenItemCardChoices();

            else if (itemPickupType == ItemPickupType.WEAPON)
                ItemManager.Instance.OpenWeaponCardChoices();

            Destroy(gameObject);
        }
    }
}
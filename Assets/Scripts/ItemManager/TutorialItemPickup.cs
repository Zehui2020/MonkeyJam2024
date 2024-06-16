using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItemPickup : MonoBehaviour
{
    [SerializeField] private Item itemToGive;
    private EntityAudioController entityAudioController;

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
            ItemManager.Instance.OpenTutorialCardChoices(itemToGive);

            Destroy(gameObject);
        }
    }
}
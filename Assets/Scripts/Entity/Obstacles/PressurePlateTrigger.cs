using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
{
    bool hasTriggered;
    [SerializeField] private GameObject[] _objects;

    EntityAudioController entityAudioController;

    private void Start()
    {
        hasTriggered = false;
        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }
        Debug.Log("Started");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter");
        //check if player
        if (!hasTriggered && other.gameObject.tag == "Player")
        {
            hasTriggered = true;

            //trigger sound
            entityAudioController.PlayAudio("pressureplate");

            //activate trigger
            foreach (GameObject obj in _objects)
            {
                obj.SetActive(! obj.activeInHierarchy);
            }
        }
    }
}

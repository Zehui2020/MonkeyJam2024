using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
{
    bool hasTriggered = false;
    [SerializeField] private GameObject[] _objects;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if player
        if (!hasTriggered && other.gameObject.tag == "Player")
        {
            hasTriggered = true;

            //activate trigger
            foreach (GameObject obj in _objects)
            {
                obj.SetActive(! obj.activeInHierarchy);
            }
        }
    }
}

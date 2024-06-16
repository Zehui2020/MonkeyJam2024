using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItemPickup : MonoBehaviour
{
    [SerializeField] private Item itemToGive;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ItemManager.Instance.OpenTutorialCardChoices(itemToGive);

            Destroy(gameObject);
        }
    }
}

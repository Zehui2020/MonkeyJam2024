using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMoneyPickup : MonoBehaviour
{
    [SerializeField] private int moneyAmount;
    [SerializeField] private MoneyPopup moneyPopup;
    [SerializeField] private Animator moneyAnimator;

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

    private void OnPickup()
    {
        PlayerController.Instance.AddMoney(moneyAmount);
        // Add money
        entityAudioController.PlayAudio("coin");
        MoneyPopup popup = Instantiate(moneyPopup, transform.position, Quaternion.identity);
        popup.SetAmount(moneyAmount);
        StartCoroutine(DestroyRoutine());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            OnPickup();
        }
    }

    private IEnumerator DestroyRoutine()
    {
        moneyAnimator.SetTrigger("collect");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}

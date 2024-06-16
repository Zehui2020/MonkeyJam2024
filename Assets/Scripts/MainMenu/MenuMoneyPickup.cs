using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMoneyPickup : MonoBehaviour
{
    [SerializeField] private MoneyPopup moneyPopup;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        MoneyPopup popup = Instantiate(moneyPopup, transform.position, Quaternion.identity);
        popup.SetAmount(20);
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        animator.SetTrigger("collect");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
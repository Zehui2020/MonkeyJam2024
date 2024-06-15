using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMoneyPickup : MonoBehaviour
{
    [SerializeField] private int moneyAmount;
    [SerializeField] private MoneyPopup moneyPopup;

    private void OnPickup()
    {
        // Add money
        MoneyPopup popup = Instantiate(moneyPopup, transform.position, Quaternion.identity);
        popup.SetAmount(moneyAmount);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            OnPickup();
            Destroy(gameObject);
        }
    }
}

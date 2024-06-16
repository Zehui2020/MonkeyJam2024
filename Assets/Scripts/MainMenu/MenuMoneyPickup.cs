using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMoneyPickup : MonoBehaviour
{
    [SerializeField] private MoneyPopup moneyPopup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        MoneyPopup popup = Instantiate(moneyPopup, transform.position, Quaternion.identity);
        popup.SetAmount(20);
    }
}
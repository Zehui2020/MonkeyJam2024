using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amount;

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    public void SetAmount(int money)
    {
        amount.text = "+ " + money.ToString();
        StartCoroutine(DestroyRoutine());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float launchForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            return;

        rb.AddForce(launchForce * Vector3.up, ForceMode2D.Impulse);
    }
}

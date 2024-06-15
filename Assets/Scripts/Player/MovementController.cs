using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D frontTireRB;
    [SerializeField] private Rigidbody2D backTireRB;
    private Rigidbody2D playerRB;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;


    public void InitMovementController()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        frontTireRB.AddTorque(moveSpeed * Time.fixedDeltaTime);
        backTireRB.AddTorque(moveSpeed * Time.fixedDeltaTime);
        playerRB.AddTorque(rotationSpeed * Time.fixedDeltaTime);
    }
}
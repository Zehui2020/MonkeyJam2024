using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = -Input.GetAxisRaw("Rotation");
        float verticalAxis = Input.GetAxisRaw("Horizontal");
        Vector3 dir = new Vector3 (horizontalAxis, verticalAxis, 0);
        transform.position += dir * speed * Time.deltaTime;
    }
}

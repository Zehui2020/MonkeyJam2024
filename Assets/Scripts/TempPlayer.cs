using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    // Start is called before the first frame update
    private EntityAudioController controller;

    void Start()
    {
        controller = GetComponent<EntityAudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = 0;
        float verticalAxis = 0;

        if (Input.GetKey(KeyCode.W))
        {
            verticalAxis = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalAxis = -1;
        }

        if (Input.GetKey(KeyCode.D))
        { horizontalAxis = 1; }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontalAxis = -1;
        }


        Vector3 dir = new Vector3 (horizontalAxis, verticalAxis, 0);

        //if moving
        if (dir.magnitude > 0)
        {
            //play sound
            controller.PlayAudio("cycling");
        }
        else
        {
            //stop sound
            controller.StopAudio("cycling");
        }

        transform.position += dir * speed * Time.deltaTime;
    }
}

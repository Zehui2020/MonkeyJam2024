using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that handles player movement, inputs & physics
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        Initialise();
    }

    //DEBUGGING PLS REMOVE
    // Update is called once per frame
    void Update()
    {
        UpdatePlayer();
        if (Input.GetAxis("Horizontal") != 0)
        {
            Movement(Input.GetAxis("Horizontal"));
        }
    }
    //Code for initialising in Game Controller
    public void Initialise()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    //Code to update player in Game Controller
    public void UpdatePlayer()
    {
        rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -2f, 2f), Mathf.Clamp(rigidBody.velocity.y, -2f, 2f));
    }
    //Movement input from GameControlller
    public void Movement(float movement)
    {
        rigidBody.AddForce(new Vector2(movement * Time.deltaTime * speed, 0),ForceMode2D.Impulse);
    }
}

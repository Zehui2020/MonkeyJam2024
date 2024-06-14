using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that handles player movement, inputs & physics
public class PlayerController : MonoBehaviour
{
    //SerializeField Speed of Bike, BrakeForce, Jump Force, rotation AirForce & RotationSpeed of bike
    [SerializeField] float speed;
    [SerializeField] float brakeForce;
    [SerializeField] float jumpForce;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationAirForce;
    //Rigidbody of the bicycle
    private Rigidbody2D rigidBody;
    //Checks if bike is braking & will allow player to move forward or not
    private bool isBraking;
    //Checks if wheels are grounded
    private bool isGrounded;
    //Checks if player is near anything
    private bool isPlayerNearAnything;
    //Script starts here

    //Code for initialising in Game Controller
    public void Initialise()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    public void UpdatePlayerIsNearAnything()
    {
        if (Physics2D.OverlapCircle(transform.position, 2, ~(1 << 3)))
        {
            isPlayerNearAnything = true;
        }
        else
        {
            isPlayerNearAnything = false;
        }
    }
    //Code to update player in Game Controller
    public void UpdatePlayer()
    {
        rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -10f, 10f), Mathf.Clamp(rigidBody.velocity.y, -10f, 10f));
        if (isPlayerNearAnything)
        {
            rigidBody.angularVelocity = Mathf.Clamp(rigidBody.angularVelocity, -60f, 60f);
        }
        else
        {
            rigidBody.angularVelocity = Mathf.Clamp(rigidBody.angularVelocity, -60f * rotationAirForce, 60f * rotationAirForce);
        }
        if (rigidBody.velocity.x > 0.05)
        {
            transform.localScale = new Vector3(-2, 2, 0);
        }
        else if (rigidBody.velocity.x < -0.05f)
        {
            transform.localScale = new Vector3(2, 2, 0);
        }
    }
    //Movement input from GameControlller
    public void Movement(MovementAxisCommand _movementAxisCommand)
    {
        if (!isBraking && isGrounded)
        {
            rigidBody.AddForce(new Vector2(_movementAxisCommand.HorizontalAxis * Time.deltaTime * speed, 0), ForceMode2D.Impulse);
        }
    }
    //Braking input from GameControlller
    public void Braking()
    {
        rigidBody.AddForce(new Vector2(-rigidBody.velocity.x * Time.deltaTime * brakeForce, 0), ForceMode2D.Impulse);
    }
    //Jump input from GameController
    public void Jump()
    {
        if (isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    //Setting boolean of braking
    public void SetBraking(bool newBraking)
    {
        isBraking = newBraking;
    }
    //Adding torque for rotation of bike, rotation input from GameControlller
    public void Rotation(RotationAxisCommand rotationAxisCommand)
    {
        if (isPlayerNearAnything)
        {
            rigidBody.AddTorque(rotationAxisCommand.RotationAxis * Time.deltaTime * rotationSpeed, ForceMode2D.Impulse);
        }
        else
        {
            rigidBody.AddTorque(rotationAxisCommand.RotationAxis * Time.deltaTime * rotationSpeed * rotationAirForce, ForceMode2D.Impulse);
        }
    }
    //triggers for ground detection
    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }
}

using UnityEngine;
//Commands
public class MovementAxisCommand
{
    public float HorizontalAxis { get; private set; }

    public MovementAxisCommand(float horizontalAxis)
    {
        HorizontalAxis = horizontalAxis;
    }
}
public class RotationAxisCommand
{
    public float RotationAxis { get; private set; }

    public RotationAxisCommand(float rotationAxis)
    {
        RotationAxis = rotationAxis;
    }
}
public class InputController : MonoBehaviour
{
    //Character Movement
    public bool TryGetMovementAxisInput(out MovementAxisCommand movementAxisCommand)
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        movementAxisCommand = new MovementAxisCommand(horizontalAxis);

        return horizontalAxis != 0;
    }

    //Character Rotation
    public bool TryGetRotationAxisInput(out RotationAxisCommand rotationAxisCommand)
    {
        float rotationAxis = Input.GetAxis("Rotation");
        rotationAxisCommand = new RotationAxisCommand(rotationAxis);

        return rotationAxis != 0;
    }
    //Jump
    public bool TryGetJump()
    {
        return Input.GetAxis("Jump") != 0;
    }
    //Braking
    public bool TryGetBraking()
    {
        return Input.GetAxis("Brake") != 0;
    }
    //Attacking
    public bool TryGetAttacking()
    {
        return Input.GetAxis("Fire1") != 0;
    }
}

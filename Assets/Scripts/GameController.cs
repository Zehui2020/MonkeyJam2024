using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Serializing controller for main gameplay
    [SerializeField] PlayerController _playerController;
    [SerializeField] InputController _inputController;

    //Initialising conrollers
    void Start()
    {
        _playerController.Initialise();
    }

    //Updating controllers
    void Update()
    {
        //Updating player is near anything
        _playerController.UpdatePlayerIsNearAnything();
        //Getting Movement
        MovementAxisCommand movementAxisCommand;
        if (_inputController.TryGetMovementAxisInput(out movementAxisCommand))
        {
            _playerController.Movement(movementAxisCommand);
        }
        //Getting Rotation
        RotationAxisCommand rotationAxisCommand;
        if (_inputController.TryGetRotationAxisInput(out rotationAxisCommand))
        {
            _playerController.Rotation(rotationAxisCommand);
        }
        //Getting Braking
        if (_inputController.TryGetBraking())
        {
            _playerController.Braking();
            _playerController.SetBraking(true);
        }
        else
        {
            _playerController.SetBraking(false);
        }
        //Getting Jumping
        if (_inputController.TryGetJump())
        {
            _playerController.Jump();
        }
        //Updating Player
        _playerController.UpdatePlayer();
    }
}

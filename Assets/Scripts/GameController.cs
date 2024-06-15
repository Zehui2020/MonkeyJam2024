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
        //Update Projectiles
        ProjectileManager.instance.UpdateProjectile();

        //Getting Movement
        MovementAxisCommand movementAxisCommand;
        if (_inputController.TryGetMovementAxisInput(out movementAxisCommand))
        {
            _playerController.Movement(movementAxisCommand);
        }
        else
        {
            _playerController.SetPlayerMovement(0);
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
        //Getting attack
        if (_inputController.TryGetAttacking())
        {
            _playerController.UseWeapon();
        }
        //Updating Player
        _playerController.UpdatePlayer();
    }
}

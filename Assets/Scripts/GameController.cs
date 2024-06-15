using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Serializing controller for main gameplay
    [SerializeField] PlayerController _playerController;
    [SerializeField] InputController _inputController;
    [SerializeField] PlayerHealth _playerHealth;
    //Initialising conrollers
    void Start()
    {
        _playerController.Initialise();
        _playerHealth.Initialise();
    }

    //Updating controllers
    void Update()
    {
        //Update Projectiles
        ProjectileManager.instance.UpdateProjectile();
        //Updating player is near anything
        _playerController.UpdatePlayerIsNearAnything();
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
        //Updating Player health
        _playerHealth.UpdatePlayerHealth();
    }
}

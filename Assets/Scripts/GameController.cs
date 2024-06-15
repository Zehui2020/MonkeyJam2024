using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Serializing controller for main gameplay
    [SerializeField] PlayerController _playerController;
    [SerializeField] InputController _inputController;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerAnimation _playerAnimation;
    [SerializeField] ItemManager _itemManager;
    //Initialising conrollers
    void Start()
    {
        _playerController.Initialise();
        _playerHealth.Initialise();
        _itemManager.InitItemManager();
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
            _playerAnimation.SetMoving(true);
        }
        else
        {
            _playerController.SetPlayerMovement(0);
            _playerAnimation.SetMoving(false);
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
            _playerAnimation.SetJumping(true);
        }
        else
        {
            _playerAnimation.SetJumping(false);
        }
        //Getting attack
        if (_inputController.TryGetAttacking())
        {
            _playerController.UseWeapon();
        }
        // Interact
        if (_inputController.TryGetInteract())
        {
            _playerController.OnInteract();
        }
        //Updating Player
        _playerController.UpdatePlayer();
        //Updating Player health
        _playerHealth.UpdatePlayerHealth();
        //Updating Player Aniamtions
        _playerAnimation.UpdatePlayerAnimation();
    }
}

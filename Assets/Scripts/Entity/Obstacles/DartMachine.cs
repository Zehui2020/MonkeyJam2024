using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMachine : Entity
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float fireRate = 5;
    [SerializeField] private Transform firePos;

    enum ShootDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField] private ShootDirection _direction;

    private float counter;
    public override void Init()
    {
        hasInit = true;
        counter = 0;

        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }
    }

    public override void HandleUpdate(float _distortTime)
    {
        counter += Time.deltaTime * _distortTime;

        if (counter > fireRate) 
        {
            entityAudioController.PlayAudio("dart");
            //reset
            counter = 0;
            //fire projectile
            Projectile proj = ProjectileManager.instance.GetProjectile(ProjectileManager.ProjectileType.Bullet);
            

            //rotate object
            //originally looking downward
            switch (_direction)
            {
                /*case ShootDirection.Left:
                    break;*/
                case ShootDirection.Right:
                    proj.Shoot("Enemy", transform.right, transform.position);
                    break;
                case ShootDirection.Up:
                    proj.Shoot("Enemy", transform.up, transform.position);
                    break;
                case ShootDirection.Down:
                    proj.Shoot("Enemy", -transform.up, transform.position);
                    break;
                default:
                    break;
            }

        }
    } 
}

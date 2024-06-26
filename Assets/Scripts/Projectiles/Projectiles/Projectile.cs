using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    //speed of projectile
    [SerializeField] protected float speed;
    //how long the projectile lasts if it doesnt hit an enemy
    [SerializeField] protected float aliveTime;
    protected float currAliveTime;
    //who the projectile belongs to
    protected string ownerName;
    //direction projectile moves to
    protected Vector3 direction;

    [SerializeField] protected int damage;
    protected int currDamage;
    [SerializeField] protected bool shouldBeFlipped;
    //updates the projectile (each projectile has their own update function)

    //sound
    protected EntityAudioController entityAudioController;

    private void Start()
    {
        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }
    }

    public abstract void UpdateProjectile();

    //when the weapon shoots this projectile and all relevant info
    public virtual void Shoot(string newOwnerName, Vector3 newDirection, Vector3 newPosition)
    {
        ownerName = newOwnerName;
        direction = newDirection;
        transform.position = newPosition;
        currAliveTime = aliveTime;
        transform.right = (direction.x < 0 || shouldBeFlipped) ? -newDirection : newDirection;
        currDamage = damage;

        //check if enemy
        if (newOwnerName == "Enemy")
        {
            //slow down speed of bullet by half
            direction *= 0.5f;
        }
    }
    public virtual void Multiplier(float _multiplier)
    {
        currDamage = (int)(currDamage * _multiplier);
    }
}

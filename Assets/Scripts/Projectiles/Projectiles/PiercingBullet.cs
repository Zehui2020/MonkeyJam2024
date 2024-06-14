using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBullet : Projectile
{
    [SerializeField] float durability;
    float currDurability;
    public override void UpdateProjectile()
    {
        currAliveTime -= Time.deltaTime;
        if (currAliveTime <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        transform.position += speed * Time.deltaTime * direction;
    }
    public override void Shoot(string newOwnerName, Vector3 newDirection, Vector3 newPosition)
    {
        base.Shoot(newOwnerName, newDirection, newPosition);
        currDurability = durability;
    }
}

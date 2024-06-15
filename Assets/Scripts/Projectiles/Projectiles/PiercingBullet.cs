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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon")) //ignore all weapons
        {
            return;
        }
        if (ownerName.Equals("Player"))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyEntity>().Damage(damage);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return;
            }
        }
        else if (ownerName.Equals("Enemy"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerHealth>().AddHealth(-1);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                return;
            }
        }
        currDurability--;
        if (currDurability <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

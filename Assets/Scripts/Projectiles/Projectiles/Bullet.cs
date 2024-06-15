using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (ownerName.Equals("Player"))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyEntity>().Damage(1);
            }
        }
        else if (ownerName.Equals("Enemy"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                
            }
        }
    }
}

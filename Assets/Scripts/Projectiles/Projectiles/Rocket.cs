using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] float explosionRadius;
    [SerializeField] GameObject explosionGO;
    [SerializeField] float explodeEffectTime;
    float currExplodeEffectTime;
    bool hasExploded;
    public override void UpdateProjectile()
    {
        currAliveTime -= Time.deltaTime;
        if (!hasExploded)
        {
            if (currAliveTime <= 0)
            {

                explosionGO.SetActive(true);
                explosionGO.GetComponent<Explosion>().Explode(explosionRadius, transform.position, ownerName,damage);
                hasExploded = true;
                currExplodeEffectTime = explodeEffectTime;
            }
            transform.position += speed * Time.deltaTime * direction;
        }
        else if (currExplodeEffectTime <= 0)
        {
            gameObject.SetActive(false);
            explosionGO.SetActive(false);
        }
        else
        {
            currExplodeEffectTime -= Time.deltaTime;
        }
        
    }
    public override void Shoot(string newOwnerName, Vector3 newDirection, Vector3 newPosition)
    {
        base.Shoot(newOwnerName, newDirection, newPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            return;
        }
        if (other.CompareTag(ownerName))
        {
            return;
        }
        explosionGO.SetActive(true);
        explosionGO.GetComponent<Explosion>().Explode(explosionRadius, transform.position, ownerName, damage);
        hasExploded = true;
        currExplodeEffectTime = explodeEffectTime;
    }
}

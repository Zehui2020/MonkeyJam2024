using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : Projectile
{
    private bool setFalseWhenHitEnemy;
    public override void UpdateProjectile()
    {
        entityAudioController.PlayAudio("fire");
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
        setFalseWhenHitEnemy = true;
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
                other.gameObject.GetComponent<EnemyEntity>().Damage(currDamage);
                if (!setFalseWhenHitEnemy)
                {
                    return;
                }
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
                if (!setFalseWhenHitEnemy)
                {
                    return;
                }
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                return;
            }
        }
        //turn off sound
        entityAudioController.StopAudio("fire");
        gameObject.SetActive(false);
    }
    public void SetFalseWhenHittingEnemy(bool _newSetFalseWhenHittingEnemy)
    {
        setFalseWhenHitEnemy = _newSetFalseWhenHittingEnemy;
    }
}

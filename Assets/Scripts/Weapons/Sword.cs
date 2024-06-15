using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    bool isUsing;
    [SerializeField] float maxangle;
    [SerializeField] float minangle;
    [SerializeField] float atkSpd;
    [SerializeField] int damage;
    float currAttackTime;
    string ownerName;
    public override void Initialise()
    {
        base.Initialise();
    }

    public override void UpdateGun()
    {
        base.UpdateGun();
        if (isUsing)
        {
            if (transform.lossyScale.x > 0)
            {
                transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, minangle), Quaternion.Euler(0, 0, maxangle), currAttackTime);
            }
            else
            {
                transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, maxangle), Quaternion.Euler(0, 0, minangle), currAttackTime);
            }
            currAttackTime += Time.deltaTime * atkSpd;
            if (currAttackTime >= 1)
            {
                isUsing = false;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    public override void Use(string _ownerName)
    {
        if (currAttackInterval <= 0 && currAmmo > 0)
        {
            isUsing = true;
            currAttackInterval = attackInterval;
            currAttackTime = 0;
            ownerName = _ownerName;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isUsing)
        {
            if (ownerName.Equals("Player"))
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    other.gameObject.GetComponent<EnemyEntity>().Damage(damage);
                }
            }
            else if (ownerName.Equals("Enemy"))
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    other.gameObject.GetComponent<PlayerHealth>().AddHealth(-1);
                }
            }
        }
    }
}

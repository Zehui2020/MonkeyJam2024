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
    public override bool Use(string _ownerName)
    {
        bool hasAttacked = false;
        if (currAttackInterval <= 0 && currAmmo > 0)
        {
            hasAttacked = true;
            entityAudioController.PlayAudio("melee", true);
            isUsing = true;
            if (UpgradeLevel < 2)
            {
                currAttackInterval = attackInterval * itemStats.fireRateModifier;
            }
            else
            {
                currAttackInterval = attackInterval * 0.5f * itemStats.fireRateModifier;
            }
            currAttackTime = 0;
            ownerName = _ownerName;
        }
        return hasAttacked;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isUsing)
        {
            if (ownerName.Equals("Player"))
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    if (UpgradeLevel < 1)
                    {
                        other.gameObject.GetComponent<EnemyEntity>().Damage(damage);
                    }
                    else
                    {
                        other.gameObject.GetComponent<EnemyEntity>().Damage((int)(damage * 1.5f));
                    }
                }
            }
            else if (ownerName.Equals("Enemy"))
            {
                Debug.Log("Enemy Is Using");
                Debug.Log("Tag " + other.gameObject.tag + " Name" + other.gameObject.name);
                if (other.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player HIT");
                    other.gameObject.GetComponent<PlayerHealth>().AddHealth(-1);
                }
            }
        }
    }
}

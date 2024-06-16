using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstRifle : Weapon
{
    private bool isShooting;
    [SerializeField] float maxShootingInterval;
    [SerializeField] int bulletCount;
    int currBulletCount;
    private float shootingInterval;
    string ownerName;
    public override void Initialise()
    {
        base.Initialise();
    }

    public override void UpdateGun()
    {
        base.UpdateGun();
        if (isShooting)
        {
            if (shootingInterval <= 0)
            {
                Projectile proj = ProjectileManager.instance.GetProjectile(projectileType);
                if (UpgradeLevel < 2)
                {
                    Debug.Log("shoot");
                    proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1), barrel.transform.position);
                    proj.Multiplier(damageMultiplier);
                }
                else
                {
                    proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1), barrel.transform.position + transform.up * 0.1f);
                    proj.Multiplier(damageMultiplier);
                    proj = ProjectileManager.instance.GetProjectile(projectileType);
                    proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1), barrel.transform.position + -transform.up * 0.1f);
                    proj.Multiplier(damageMultiplier);
                }
                shootingInterval = maxShootingInterval;
                currBulletCount++;
                if (currBulletCount >= ((UpgradeLevel < 1) ? bulletCount : bulletCount + 1))
                {
                    isShooting = false;
                    shootingInterval = 0;
                }
            }
            else
            {
                shootingInterval -= Time.deltaTime;
            }
        }
        if (currAutoReloadTime > 0)
        {
            currAutoReloadTime -= Time.deltaTime;
        }
        else
        {
            if (currAmmo != Mathf.CeilToInt(ammo * itemStats.magSizeModifier))
            {
                Reload();
            }
        }
    }
    public override bool Use(string _ownerName)
    {
        bool hasAttacked = false;
        if (currAttackInterval <= 0 && currAmmo > 0)
        {
            hasAttacked = true;
            entityAudioController.PlayAudio("rifle", true);
            currAmmo--;
            if (currAmmo == 0)
            {
                currReloadTime = reloadTime;
            }
            else
            {
                if (currReloadTime > 0)
                {
                    currReloadTime = 0;
                }
            }
            isShooting = true;
            currBulletCount = 0;
            ownerName = _ownerName;
            shootingInterval = 0;
            
        }
        currAutoReloadTime = autoReloadTime;
        return hasAttacked;
    }
}

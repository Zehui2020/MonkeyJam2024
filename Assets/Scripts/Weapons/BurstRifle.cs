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
                }
                else
                {
                    proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1), barrel.transform.position + transform.up * 0.1f);
                    proj = ProjectileManager.instance.GetProjectile(projectileType);
                    proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1), barrel.transform.position + -transform.up * 0.1f);
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
    }
    public override void Use(string _ownerName)
    {
        if (currAttackInterval <= 0 && currAmmo > 0)
        {
            currAmmo--;
            if (currAmmo == 0)
            {
                currReloadTime = reloadTime;
            }
            isShooting = true;
            currBulletCount = 0;
            ownerName = _ownerName;
            shootingInterval = 0;
        }
    }
}

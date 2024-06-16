using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public override void Initialise()
    {
        base.Initialise();
    }

    public override void UpdateGun()
    {
        base.UpdateGun();
    }
    public override void Use(string ownerName)
    {
        if (currAttackInterval <= 0 && currAmmo > 0)
        {
            currAmmo--;
            if (currAmmo == 0)
            {
                if (UpgradeLevel < 2)
                {
                    currReloadTime = reloadTime;
                }
                else
                {
                    currReloadTime = reloadTime * 0.5f;
                }
            }
            Projectile proj = ProjectileManager.instance.GetProjectile(projectileType);
            proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1), barrel.transform.position);
            if (UpgradeLevel < 1)
            {
                currAttackInterval = attackInterval;
            }
            else
            {
                currAttackInterval = attackInterval * 0.5f;
            }
        }
    }
}

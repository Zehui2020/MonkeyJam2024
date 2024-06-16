using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Weapon
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
                if (UpgradeLevel < 1)
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
            if (UpgradeLevel > 1)
            {
                proj.GetComponent<PiercingBullet>().SetDurability(5);
            }
            currAttackInterval = attackInterval;
        }
    }
}

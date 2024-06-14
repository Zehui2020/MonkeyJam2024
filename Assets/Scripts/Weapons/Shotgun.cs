using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
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
                currReloadTime = reloadTime;
            }
            for (int projectilenum = 0; projectilenum < 3; projectilenum++)
            {
                Projectile proj = ProjectileManager.instance.GetProjectile(projectileType);
                proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1) + transform.up * 0.2f - transform.up * 0.2f * projectilenum, barrel.transform.position);
                currAttackInterval = attackInterval;
            }
        }
    }
}

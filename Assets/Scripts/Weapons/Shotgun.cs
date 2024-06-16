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
    public override void Use(string ownerName)
    {
        if (currAttackInterval <= 0 && currAmmo > 0)
        {
            entityAudioController.PlayAudio("shotgun", true);
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
            if (UpgradeLevel < 2)
            {
                for (int projectilenum = 0; projectilenum < 3; projectilenum++)
                {
                    Projectile proj = ProjectileManager.instance.GetProjectile(projectileType);
                    proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1) + transform.up * 0.2f - transform.up * 0.2f * projectilenum, barrel.transform.position);
                    proj.Multiplier(damageMultiplier);
                    currAttackInterval = attackInterval * itemStats.fireRateModifier;
                }
            }
            else
            {
                for (int projectilenum = 0; projectilenum < 5; projectilenum++)
                {
                    Projectile proj = ProjectileManager.instance.GetProjectile(projectileType);
                    proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1) + transform.up * 0.2f - transform.up * 0.1f * projectilenum, barrel.transform.position);
                    proj.Multiplier(damageMultiplier);
                    currAttackInterval = attackInterval * itemStats.fireRateModifier;
                }
            }
           
        }
        currAutoReloadTime = autoReloadTime;
    }
    public override void Upgrade()
    {
        base.Upgrade();
    }
    public override void Reload()
    {
        base.Reload();
        if (UpgradeLevel > 0)
        {
            currAmmo += 2;
        }
    }
}

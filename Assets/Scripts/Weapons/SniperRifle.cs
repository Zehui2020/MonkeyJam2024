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
    public override bool Use(string ownerName)
    {
        bool hasAttacked = false;
        if (currAttackInterval <= 0 && currAmmo > 0)
        {
            hasAttacked = true;
            entityAudioController.PlayAudio("sniper", true);
            currAmmo--;
            if (ownerName == "Enemy")
            {
                currAmmo = 0;
            }
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
            else
            {
                if (currReloadTime > 0)
                {
                    currReloadTime = 0;
                }
            }
            Projectile proj = ProjectileManager.instance.GetProjectile(projectileType);
            proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1), barrel.transform.position);
            proj.Multiplier(damageMultiplier);
            if (UpgradeLevel > 1)
            {
                proj.GetComponent<PiercingBullet>().SetDurability(5);
            }
            currAttackInterval = attackInterval * itemStats.fireRateModifier;
        }
        currAutoReloadTime = autoReloadTime;
        return hasAttacked;
    }
}

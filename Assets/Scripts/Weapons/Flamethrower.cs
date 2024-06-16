using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] float space;
    private int currentSpace;
    public override void Initialise()
    {
        base.Initialise();
        currentSpace = 0;
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
            Projectile proj = ProjectileManager.instance.GetProjectile(projectileType);
            proj.Shoot(ownerName, transform.right * ((transform.lossyScale.x < 0) ? 1 : -1), barrel.transform.position + (maxHeight - (maxHeight - minHeight) / space * currentSpace) * transform.up);
            if (UpgradeLevel > 1)
            {
                proj.GetComponent<Flame>().SetFalseWhenHittingEnemy(false);
            }
            currAttackInterval = attackInterval;
            currentSpace++;
            if (currentSpace >= space)
            {
                currentSpace = 0;
            }
        }
    }
    public override void Reload()
    {
        base.Reload();
        if (UpgradeLevel > 0)
        {
            currAmmo += 25;
        }
    }
}

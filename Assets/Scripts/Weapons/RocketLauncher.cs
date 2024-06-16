using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    [SerializeField] float moveBackForce;
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
            if (currAmmo != ammo)
            {
                Reload();
            }
        }
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
            if (UpgradeLevel > 0)
            {
                proj.GetComponent<Rocket>().SetSpeed(6);
                if (UpgradeLevel > 1)
                {
                    proj.GetComponent<Rocket>().SetExplosionRadius(25);
                }
            }
            currAttackInterval = attackInterval;
            GetComponentInParent<Rigidbody2D>().AddForce((transform.parent.position - (transform.position + transform.right * ((transform.lossyScale.x < 0) ? 1 : -1))).normalized * moveBackForce, ForceMode2D.Impulse);
        }
        currAutoReloadTime = autoReloadTime;
    }
}

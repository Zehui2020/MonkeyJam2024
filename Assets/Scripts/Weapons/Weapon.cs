using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class for weapns
public abstract class Weapon : MonoBehaviour
{
    //Ammo management
    [SerializeField] protected int ammo;
    protected int currAmmo;

    //Reload Management
    [SerializeField] protected float reloadTime;
    protected float currReloadTime;

    //shooting interval
    [SerializeField] protected float attackInterval;
    protected float currAttackInterval;

    //Type of projectile weapon uses
    [SerializeField] protected ProjectileManager.ProjectileType projectileType;

    //Where the projectile will spawn from
    [SerializeField] protected GameObject barrel;

    //Upgrade levels for different weapon levela
    int UpgradeLevel;

    //script starts here

    //initialise weapon
    public virtual void Initialise()
    {
        currAmmo = ammo;
        currReloadTime = 0;
        UpgradeLevel = 1;
    }

    //updates the gun
    public virtual void UpdateGun()
    {
        if (currReloadTime > 0)
        {
            currReloadTime -= Time.deltaTime;
            if (currReloadTime <= 0)
            {
                currAmmo = ammo;
            }
        }
        if (currAttackInterval > 0)
        {
            currAttackInterval -= Time.deltaTime;
        }
    }

    //When any entity uses the Gun
    public abstract void Use(string ownerName);
}

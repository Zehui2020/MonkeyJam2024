using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class for weapns
public abstract class Weapon : MonoBehaviour
{
    //Identifier of weapon
    [SerializeField] string weaponName;
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

    //To detect range to attack player (enemy)
    [SerializeField] float range;

    //Upgrade levels for different weapon levela
    //remove serializefield after debuggin
    [SerializeField] protected int UpgradeLevel;

    //script starts here

    //initialise weapon
    public virtual void Initialise()
    {
        currAmmo = ammo;
        currReloadTime = 0;
        UpgradeLevel = 0;
    }

    //updates the gun
    public virtual void UpdateGun()
    {
        if (currReloadTime > 0)
        {
            currReloadTime -= Time.deltaTime;
            if (currReloadTime <= 0)
            {
                Reload();
            }
        }
        if (currAttackInterval > 0)
        {
            currAttackInterval -= Time.deltaTime;
        }
    }

    //When any entity uses the Gun
    public abstract void Use(string ownerName);

    //upgrading weapon
    public virtual void Upgrade()
    {
        if (UpgradeLevel < 2)
        {
            UpgradeLevel++;
            Reload();
        }
    }

    //Getting weapon name
    public string GetName()
    {
        return weaponName;
    }

    //Getting weapon upgrade level
    public int GetUpgradeLevel()
    {
        return UpgradeLevel;
    }

    public virtual void Reload()
    {
        currAmmo = ammo;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class for weapns
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected ItemStats itemStats;

    public WeaponItem weaponItem;
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
    [SerializeField] public float range;

    //For automatic reload
    [SerializeField] protected float autoReloadTime;
    protected float currAutoReloadTime;
    //Upgrade levels for different weapon levela
    //remove serializefield after debuggin
    protected int UpgradeLevel;

    //damage multiplier
    [SerializeField] protected float damageMultiplier;
    //script starts here

    //sound
    protected EntityAudioController entityAudioController;

    //initialise weapon
    public virtual void Initialise()
    {
        currAmmo = Mathf.CeilToInt(ammo * itemStats.magSizeModifier);
        currReloadTime = 0;
        UpgradeLevel = 0;

        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }
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
    public abstract bool Use(string ownerName);

    //upgrading weapon
    public virtual void Upgrade()
    {
        if (UpgradeLevel < 2)
        {
            UpgradeLevel++;
            Reload();
        }
        else
        {
            UpgradeLevel++;
            AddDamageMultiplier(0.1f);
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
        return UpgradeLevel + 1;
    }

    public virtual void Reload()
    {
        entityAudioController.PlayAudio("reload");
        currAmmo = Mathf.CeilToInt(ammo * itemStats.magSizeModifier);
    }
    public void StartReload()
    {
        currReloadTime = reloadTime;
    }
    public void AddDamageMultiplier(float _increment)
    {
        damageMultiplier += _increment;
    }

    public string GetAmmoString()
    {
        return currAmmo.ToString() + " / " + Mathf.CeilToInt(ammo * itemStats.magSizeModifier).ToString();
    }
}
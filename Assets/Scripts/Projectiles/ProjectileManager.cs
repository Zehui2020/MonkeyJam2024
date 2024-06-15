using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//List per projectile because certain guns uses certain projectiles
//Uses object pulling this way therefore start & end
[System.Serializable]
public class ProjectileList
{
    public List<Projectile> _listOfProjectiles;
    public Projectile _projectilePrefab;
    public int start = 0;
    public int end = 0;
}

//Projectile Manager oversees all projectiles
public class ProjectileManager : MonoBehaviour
{
    //singleton
    public static ProjectileManager instance { get; private set; }

    //The different types of projectiles for easier reading
    public enum ProjectileType
    {
        Bullet,
        PiercingBullet,
        Rocket,
        Flame
    }
    //List of projectile lists for use later
    [SerializeField] List<ProjectileList> _listOfProjectileLists;

    //Script starts here

    //Setting singleton
    private void Awake()
    {
        instance = this;
    }

    //Updating every projectile that is currently active using start and end
    public void UpdateProjectile()
    {
        foreach (ProjectileList list in _listOfProjectileLists)
        {
            int current = list.start;
            while (current != list.end)
            {
                if (!list._listOfProjectiles[current].gameObject.activeSelf)
                {
                    if (current == list.start)
                    {
                        list.start++;
                        if (list.start >= list._listOfProjectiles.Count)
                        {
                            list.start = 0;
                        }
                    }
                }
                else
                {
                    list._listOfProjectiles[current].UpdateProjectile();
                }
                current++;
                if (current >= list._listOfProjectiles.Count)
                {
                    current = 0;
                }
            }
        }
    }

    //returns latest projectile available or reuses the oldest projectile that is used if not enough
    public Projectile GetProjectile(ProjectileType type)
    {
        Projectile projectile = _listOfProjectileLists[(int)type]._listOfProjectiles[_listOfProjectileLists[(int)type].end];
        _listOfProjectileLists[(int)type]._listOfProjectiles[_listOfProjectileLists[(int)type].end].gameObject.SetActive(true);
        _listOfProjectileLists[(int)type].end++;
        if (_listOfProjectileLists[(int)type].end >= _listOfProjectileLists[(int)type]._listOfProjectiles.Count)
        {
            _listOfProjectileLists[(int)type].end = 0;
        }
        if (_listOfProjectileLists[(int)type].end == _listOfProjectileLists[(int)type].start)
        {
            _listOfProjectileLists[(int)type].start++;
            if (_listOfProjectileLists[(int)type].start >= _listOfProjectileLists[(int)type]._listOfProjectiles.Count)
            {
                _listOfProjectileLists[(int)type].start = 0;
            }
        }
        return projectile;
    }
}

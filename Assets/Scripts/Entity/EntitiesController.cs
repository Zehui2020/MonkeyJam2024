using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TAN YI JIE
// Handles all entity updates
//Entities: Enemies, Obstacles
public class EntitiesController : MonoBehaviour
{
    [SerializeField] protected GameObject[] _weapons;
    //Variables
    //List of entities
    public List<Entity> _entities;
    private List<Entity> _entitiesToDelete;

    //Singleton Instance
    public static EntitiesController instance;
    public static EntitiesController Instance { get { return instance; } private set { instance = value; } }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }

        _entities = new List<Entity>();
        _entitiesToDelete = new List<Entity>();
    }

    

    public void Init()
    {
        
    }

    public void ControllerUpdate()
    {
        //Update all entities
        foreach (Entity e in _entities)
        {
            //check if entity has been initiated
            if (!e.hasInit)
            {
                //check if entity is enemy entity
                //EnemyEntity enemyEntity = (EnemyEntity)e;
                if (e is EnemyEntity enemyEntity)
                {
                    //assign random weapon
                    enemyEntity.assignWeapon(_weapons[UnityEngine.Random.Range(0, _weapons.Length)]);
                    
                    
                }

                //initiate uninitiated entities
                e.Init();
            }
            //update entities
            e.HandleUpdate(1);

            //check if entity needs to be deleted
            if (e.canDestroy)
            {
                _entitiesToDelete.Add(e);
            }
        }

        //delete all entities to destroy
        bool clear = false;
        foreach (Entity e in _entitiesToDelete)
        {
            _entities.Remove(e);
            Destroy(e);
            clear = true;
        }
        if (clear)
        {
            _entitiesToDelete.Clear();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        ControllerUpdate();
    }

    internal void AddEntity(Entity entity)
    {
        //add entity to controller
        _entities.Add(entity);
    }
}

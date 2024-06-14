using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TAN YI JIE
// Handles all entity updates
//Entities: Enemies, Obstacles
public class EntitiesController : MonoBehaviour
{
    //Variables
    //List of entities
    private List<Entity> _entities;


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
                //initiate uninitiated entities
                e.Init();
            }
            //update entities
            e.HandleUpdate();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Base entity stats and function
public abstract class Entity : MonoBehaviour
{
    public bool hasInit;

    private void Start()
    {
        hasInit = false;
        //add to entities controller
        EntitiesController.Instance.AddEntity(this);
    }

    public abstract void Init();
    public abstract void HandleUpdate();
    //each entity will have their own movment behaviour
}

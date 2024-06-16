using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Base entity stats and function
public abstract class Entity : MonoBehaviour
{
    public bool hasInit;
    public bool canDestroy; // entity is in a state where it needs to be removed or deleted

    //sound
    protected EntityAudioController entityAudioController; 

    private void Start()
    {
        canDestroy = false;
        hasInit = false;
        //add to entities controller
        EntitiesController.Instance.AddEntity(this);
    }

    public abstract void Init();
    public abstract void HandleUpdate(float _distortTime);
    //each entity will have their own movment behaviour
}

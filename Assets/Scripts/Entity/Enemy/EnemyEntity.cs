using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
//Yi Jie
//Base Enemy Stats and functions
public abstract class EnemyEntity : Entity
{
    [Header("Enemy Entity")]
    [SerializeField] protected LayerMask playerLayer;

    //waypoints
    [SerializeField] protected Transform[] _waypoints;
    protected int currWaypoint;

    //State
    protected enum EnemyStates
    {
        None,
        Idle,
        Patrol,
        Chase,
        Attack,
        Death,
        TotalStates
    }
    protected EnemyStates state;
    //target entity is chasing after or attacking
    protected Transform targetTransform;
    protected Vector3 targetLastSeenPos;

    //Stats
    [SerializeField] protected int health;
    [SerializeField] protected int speed; 
    [SerializeField] protected float detectTargetRange = 5;
    protected bool isStunned;

    protected float counter;
    protected float idleTimer;

    //Pathfinding
    protected Path path;
    protected int currentChaseWaypoint;
    protected bool reachedEndOfPath = false;
    protected Seeker seeker;
    protected Rigidbody2D rb;

    //Fixed Data
    protected float STUNNEDDURATION = 3;
    protected float DEATHDURATION = 0.5f;

    public void Damage(int _amt)
    {
        health -= _amt;

        //check health amount
        if (health <= 0 && state != EnemyStates.Death)
        {
            //to death state
            state = EnemyStates.Death;
            counter = 0;
        }
    }

    public void Stun()
    {
        if (!isStunned)
        {
            isStunned = true;
            counter = 0;
        }
    }

    protected void DeathStateUpdate(float _distortTime)
    {
        //death anim
        counter += Time.deltaTime * _distortTime;
        if (counter >= DEATHDURATION)
        {
            canDestroy = true;
        }
    }

    protected void StartChase()
    {
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    protected void StopChase()
    {
        CancelInvoke("UpdatePath");
    }

    protected void OnPathComplete(Path p)
    {
        //check if path has any errors
        if (!p.error)
        {
            //no error = create/assign new path
            path = p;
            currentChaseWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        //check if seeker is not currently calculating a path
        if (seeker.IsDone())
            seeker.StartPath(rb.position, targetLastSeenPos, OnPathComplete);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Yi Jie
//Base Enemy Stats and functions
public abstract class EnemyEntity : Entity
{
    [Header ("Enemy Entity")]
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
    protected EnemyStates state, prevState;

    //Stats
    [SerializeField] protected int health;
    [SerializeField] protected int speed;

    protected float counter;
    protected float idleTimer;
    
    public void Damage(int _amt)
    {
        health -= _amt;

        //check health amount

    }
}

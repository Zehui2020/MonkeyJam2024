using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : EnemyEntity
{
    public override void Init()
    {
        hasInit = true;
        state = EnemyEntity.EnemyStates.Idle;
        idleTimer = Random.Range(2, 5);
        currWaypoint = 0;
    }

    public override void HandleUpdate()
    {
        switch (state)
        {
            //Idle
            case EnemyStates.Idle:
                //Stand Still / Rest
                counter += Time.deltaTime;
                //prev: attack/Chase: look for player then go back to patrol
                if (counter >= idleTimer)
                {
                    //go to patrol
                    state = EnemyStates.Patrol;
                    //reset counter
                    counter = 0;
                }
                break;
            //Patrol
            case EnemyStates.Patrol:
                //Walk towards waypoints
                //Get Direction
                Vector3 dir = _waypoints[currWaypoint].position - transform.position;
                dir.y = 0;
                dir.Normalize();
                //move towards waypoint
                transform.position += dir * speed * Time.deltaTime;
                //check if reach waypoint
                if (Vector3.Distance(transform.position, _waypoints[currWaypoint].position) <= 0.5f)
                {
                    //go to idle mode
                    state = EnemyStates.Idle;
                    //increment current  
                    currWaypoint = (currWaypoint + 1) % _waypoints.Length;
                }
                //Can see player if get too close
                break;
            //Chase
            case EnemyStates.Chase:
                //Scream and start chasing
                //go to last player position
                break;
            //Attack
            case EnemyStates.Attack:
                //In range, do attack
                break;
            //Death
            case EnemyStates.Death:
                //death anim
                break;
        }
    }
}

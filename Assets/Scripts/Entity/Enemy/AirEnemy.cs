using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemy : EnemyEntity
{
    public override void Init()
    {
        hasInit = true;
        state = EnemyEntity.EnemyStates.Idle;
        idleTimer = Random.Range(4, 6);
        currWaypoint = 0;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void HandleUpdate(float _distortTime)
    {
        //check if stun
        if (isStunned)
        {
            counter += Time.deltaTime * _distortTime;
            //check if stun finish
            if (counter >= STUNNEDDURATION)
            {
                //stun complete
                isStunned = false;
                counter = 0;
            }
            else
            {
                return;
            }
        }

        switch (state)
        {
            //Idle
            case EnemyStates.Idle:
                //Stand Still / Rest
                counter += Time.deltaTime * _distortTime;
                //prev: attack/Chase: look for player then go back to patrol
                if (counter >= idleTimer)
                {
                    //go to patrol
                    state = EnemyStates.Patrol;
                    //reset counter
                    counter = 0;
                }
                //see player if player is closer
                //Can see player if get too close
                Debug.Log("Scan");
                Collider2D c = Physics2D.OverlapCircle(transform.position, detectTargetRange + 1, playerLayer);
                //detected player
                if (c != null)
                {
                    Debug.Log("Detected");
                    targetTransform = c.transform;
                    //chase player
                    state = EnemyStates.Chase;
                    targetLastSeenPos = targetTransform.position;
                    StartChase();
                }
                break;
            //Patrol
            case EnemyStates.Patrol:
                //Walk towards waypoints
                //Get Direction
                Vector3 dir = _waypoints[currWaypoint].position - transform.position;
                dir.Normalize();
                //move towards waypoint
                //transform.position += dir * speed * Time.deltaTime * _distortTime;
                rb.AddForce(dir * speed * Time.deltaTime * _distortTime);
                //check if reach waypoint
                if (Vector3.Distance(transform.position, _waypoints[currWaypoint].position) <= 0.5f)
                {
                    //go to idle mode
                    state = EnemyStates.Idle;
                    //increment current  
                    currWaypoint = (currWaypoint + 1) % _waypoints.Length;
                    idleTimer = Random.Range(4, 6);
                }
                //Can see player if get too close
                Collider2D c1 = Physics2D.OverlapCircle(transform.position, detectTargetRange, playerLayer);
                //detected player
                if (c1 != null)
                {
                    Debug.Log("Detected");
                    targetTransform = c1.transform;
                    //chase player
                    state = EnemyStates.Chase;
                    targetLastSeenPos = targetTransform.position;
                    StartChase();
                }
                break;
            //Chase
            case EnemyStates.Chase:
                Debug.Log("Chase");

                //check target still in range
                if (Vector3.Distance(targetTransform.position, transform.position) <= detectTargetRange)
                {
                    //update last seen position
                    targetLastSeenPos = targetTransform.position;
                }
                //Scream and start chasing
                //go to last player position

                //check if there is path
                if (path == null)
                    return;

                if (currentChaseWaypoint >= path.vectorPath.Count)
                {
                    reachedEndOfPath = true;
                    //Idle
                    state = EnemyStates.Idle;
                    idleTimer = Random.Range(4, 6);
                    //stop tracking
                    StopChase();
                    return;
                }
                else
                {
                    reachedEndOfPath = false;
                }
                Vector2 direction = ((Vector2)path.vectorPath[currentChaseWaypoint] - rb.position);
                direction.Normalize();
                //rb.position += direction * speed * Time.deltaTime;
                rb.AddForce(direction * speed * Time.deltaTime * _distortTime);

                //check if reached waypoint
                if (Vector2.Distance(rb.position, (Vector2)path.vectorPath[currentChaseWaypoint]) <= 0.5f)
                {
                    //go to next check point
                    currentChaseWaypoint++;
                }

                break;
            //Attack
            case EnemyStates.Attack:
                //In range, do attack
                break;
            //Death
            case EnemyStates.Death:
                DeathStateUpdate(_distortTime);
                break;
        }
    }

    
}

using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemy : EnemyEntity
{
    //sound
    [SerializeField] private float flapTimer;
    private float flapTime;

    public override void Init()
    {
        hasInit = true;
        state = EnemyEntity.EnemyStates.Idle;
        idleTimer = Random.Range(4, 6);
        currWaypoint = 0;

        flapTime = 0;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        iFrames = 0;

        //set original scale
        ogScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }

        //animation
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();

        if (_weapon != null)
        {
            _weapon.Initialise();
        }
        else
        {
            canDestroy = true;
        }
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
        //check if not dead
        if (state != EnemyStates.Death)
        {
            //sound
            flapTime += Time.deltaTime * _distortTime;
            if (flapTime < flapTimer)
            {
                //play sound
                entityAudioController.PlayAudio("flap");
                //reset timer
                flapTime = 0;
            }
        }

        //iframes update
        if (iFrames > 0)
        {
            iFrames -= Time.deltaTime * _distortTime;
        }


        //update weapon
        _weapon.UpdateGun();

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
                //rotate
                if (dir.x > 0.1f)
                {
                    //_spriteRenderer.flipX = true;
                    transform.localScale = new Vector3(-ogScale.x, ogScale.y, ogScale.z);
                }
                else if (dir.x < -0.1f)
                {
                    //_spriteRenderer.flipX = false;
                    transform.localScale = new Vector3(ogScale.x, ogScale.y, ogScale.z);
                }
                dir.Normalize();
                //move towards waypoint
                //transform.position += dir * speed * Time.deltaTime * _distortTime;
                rb.AddForce(dir * speed * Time.deltaTime * _distortTime);
                //check if reach waypoint
                if (Vector3.Distance(transform.position, _waypoints[currWaypoint].position) <= 1.2f)
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
                if (Vector3.Distance(targetTransform.position, transform.position) <= detectTargetRange + 2)
                {
                    //update last seen position
                    targetLastSeenPos = targetTransform.position;

                    //rotate weapon towards target
                    Vector3 aimDir = (targetTransform.position - transform.position).normalized;
                    float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
                    _weapon.gameObject.transform.eulerAngles = new Vector3(0, 0, angle + (transform.localScale.x <= 0 ? 0: 180));

                    if (Physics2D.Raycast(_weapon.gameObject.transform.position, aimDir, 50, shootLayerCheck).collider.gameObject.tag == "Player")
                    {
                        //check to attack target
                        CheckAttackTarget();
                    }
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
                    _weapon.gameObject.transform.eulerAngles = Vector3.zero;
                    //stop tracking
                    StopChase();
                    return;
                }
                else
                {
                    reachedEndOfPath = false;
                }
                Vector2 direction = ((Vector2)path.vectorPath[currentChaseWaypoint] - rb.position);
                //rotate
                if (direction.x > 0.1f)
                {
                    //_spriteRenderer.flipX = true;
                    transform.localScale = new Vector3(-ogScale.x, ogScale.y, ogScale.z);
                }
                else if (direction.x < -0.1f)
                {
                    //_spriteRenderer.flipX = false;
                    transform.localScale = new Vector3(ogScale.x, ogScale.y, ogScale.z);
                }
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
                //wait 1 sec
                counter += Time.deltaTime * _distortTime;
                if (counter >= 1)
                {
                    state = EnemyStates.Chase;
                }
                break;
            //Death
            case EnemyStates.Death:
                DeathStateUpdate(_distortTime);
                break;
        }
    }

    
}

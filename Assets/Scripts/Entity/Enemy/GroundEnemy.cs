using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundEnemy : EnemyEntity
{
    [Header("Ground Enemy")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistCheck;

    bool isGrounded;

    public override void Init()
    {
        hasInit = true;
        state = EnemyEntity.EnemyStates.Idle;
        idleTimer = Random.Range(2, 5);
        currWaypoint = 0;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //Animation
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();

        isGrounded = false;

        if (_weapon != null )
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
        //Ground check
        isGrounded = Physics2D.Raycast(rb.position, -transform.up, groundDistCheck, groundLayer);
        //Debug.DrawRay(rb.position, -transform.up * groundDistCheck, Color.red, 0.5f);
        Debug.Log("IsGrounded: " + isGrounded);

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
        //update weapon
        _weapon.UpdateGun();
        
        switch (state)
        {
            //Idle
            case EnemyStates.Idle:
                //animation
                _animator.SetBool("ISWALKING", false);
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
                //Debug.Log("Scan");
                Collider2D c = Physics2D.OverlapCircle(transform.position, detectTargetRange  + 1, playerLayer);
                //detected player
                if (c != null)
                {
                    //Debug.Log("Detected");
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
                dir.y = 0;
                //Animation
                if (dir.x != 0)
                {
                    _animator.SetBool("ISWALKING", true);
                    //rotate
                    if (dir.x > 0.1f)
                    {
                        //_spriteRenderer.flipX = true;
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (dir.x < -0.1f)
                    {
                        //_spriteRenderer.flipX = false;
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else
                {
                    _animator.SetBool("ISWALKING", false);
                }
                dir.Normalize();
                

                //move towards waypoint
                transform.position += dir * speed * Time.deltaTime * _distortTime;
                //rb.AddForce(dir * speed * Time.deltaTime * _distortTime * 100);
                //check if reach waypoint
                if (Vector3.Distance(transform.position, _waypoints[currWaypoint].position) <= 0.5f)
                {
                    //go to idle mode
                    state = EnemyStates.Idle;
                    //increment current  
                    currWaypoint = (currWaypoint + 1) % _waypoints.Length;
                    idleTimer = Random.Range(2, 5);
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
                //Debug.Log("Chase");

                //check target still in range
                if (Vector3.Distance(targetTransform.position, transform.position) <= detectTargetRange + 2)
                {
                    //update last seen position
                    targetLastSeenPos = targetTransform.position;

                    //rotate weapon towards target
                    Vector3 aimDir = (targetTransform.position - transform.position).normalized;
                    float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
                    _weapon.gameObject.transform.eulerAngles = new Vector3(0, 0, angle + (transform.localScale.x == -1 ? 0 : 180));

                    //check to attack target
                    CheckAttackTarget();
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
                    idleTimer = Random.Range(2, 5);
                    //stop tracking
                    StopChase();
                    //reset rotation
                    _weapon.gameObject.transform.eulerAngles = Vector3.zero;
                    return;
                }
                else
                {
                    reachedEndOfPath = false;
                }
                Vector2 direction = ((Vector2)path.vectorPath[currentChaseWaypoint] - rb.position);
                direction.y = 0;
                //Animation
                if (direction.x != 0)
                {
                    _animator.SetBool("ISWALKING", true);
                    //rotate
                    if (direction.x > 0.1f)
                    {
                        //_spriteRenderer.flipX = true;
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (direction.x < -0.1f)
                    {
                        //_spriteRenderer.flipX = false;
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else
                {
                    _animator.SetBool("ISWALKING", false);
                }
                direction.Normalize();
                

                rb.position += direction * speed * Time.deltaTime;
                //rb.AddForce(direction * speed * Time.deltaTime * _distortTime * 100);

                //check if need to jump
                if (Mathf.Abs(path.vectorPath[currentChaseWaypoint].y - rb.position.y) >= 0.5f && Mathf.Abs(path.vectorPath[currentChaseWaypoint].x - rb.position.x) <= 2 && isGrounded)
                {
                    //Debug.Log("Jump");
                    //jump
                    rb.AddForce(transform.up * 300 * Time.deltaTime * _distortTime, ForceMode2D.Impulse);
                }

                //check if reached waypoint
                if (Vector2.Distance(rb.position, (Vector2)path.vectorPath[currentChaseWaypoint]) <= 0.5f)
                {
                    //go to next check point
                    currentChaseWaypoint++;
                }

                break;
            //Death
            case EnemyStates.Death:
                DeathStateUpdate(_distortTime);
                break;
        }

        //jump / fall
        if (rb.velocity.y > 0.05f && state != EnemyStates.Death)
        {
            _animator.SetBool("ISJUMPING", true);
            _animator.SetBool("ISFALLING", false);
        }
        else if (rb.velocity.y < -0.05f && state != EnemyStates.Death)
        {
            _animator.SetBool("ISJUMPING", false);
            _animator.SetBool("ISFALLING", true);
        }
        else
        {
            _animator.SetBool("ISJUMPING", false);
            _animator.SetBool("ISFALLING", false);
        }
    }
}

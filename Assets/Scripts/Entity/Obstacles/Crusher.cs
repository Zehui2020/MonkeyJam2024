using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reference from the thwomp from Mario

public class Crusher : Entity
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private GameObject hurtPlayerCollider;
    enum CrusherState
    {
        Idle,
        Fall,
        Rise,
        TotalStates
    }

    private CrusherState state;

    private float counter;
    //Start position
    private Vector2 startPos;
    //distance to the ground
    private float distToGround = 0;

    //components
    Rigidbody2D rb;

    public override void Init()
    {
        hasInit = true;
        counter = 0;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        state = CrusherState.Idle;

        //get starting position
        startPos = new Vector2 ( rb.position.x, rb.position.y);

        //collider
        hurtPlayerCollider.SetActive(false);

        //raycast to find ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 50, groundLayer);
        if (hit.collider != null)
        {
            //get distance
            distToGround = Vector2.Distance(rb.position, hit.point);
        }
        else
        {
            //cannot detect anyfloor, terminate this entity
            canDestroy = true;
        }
    }
    public override void HandleUpdate(float _distortTime)
    {
        Debug.Log("Update");
        //Debug.DrawRay(rb.position, -transform.up * 50, Color.red);
        switch (state)
        {
            case CrusherState.Idle:
                //check if player walks underneath
                RaycastHit2D hit = Physics2D.CircleCast(transform.position, 3f, -transform.up, distToGround, playerLayer);
                if (hit.collider != null)
                {
                    //see player, fall
                    state = CrusherState.Fall;
                    //set gravity on
                    rb.gravityScale = 1;
                    //activate hurt player collider
                    hurtPlayerCollider.SetActive(true);
                }
                break;
            case CrusherState.Fall:
                //check if reach ground
                RaycastHit2D hitGround = Physics2D.Raycast(rb.position, -transform.up, 1.45f, groundLayer);
                if (hitGround.collider != null)
                {
                    counter += Time.deltaTime * _distortTime;
                    hurtPlayerCollider.SetActive(false);
                    if (counter >= 2)
                    {
                        //reach ground
                        //go back up
                        rb.gravityScale = 0;
                        
                        state = CrusherState.Rise;

                        counter = 0;
                    }
                    
                }
                break;
            case CrusherState.Rise:
                //move towards original position
                rb.position += (startPos - rb.position).normalized * 2 * _distortTime * Time.deltaTime;
                //check if reach original position
                if (Vector2.Distance(rb.position, startPos) <= 0.1f)
                {
                    //reach original position
                    rb.position = startPos;
                    //go back to idle
                    state = CrusherState.Idle;
                }
                break;
            default:
                break;
        }
    }
}

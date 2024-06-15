using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    bool isGrounded = false;
    bool isJump = false;
    bool isMoving = false;
    public void UpdatePlayerAnimation()
    {
        if (!isGrounded)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("InAir"))
            {
                animator.Play("JumpDown");
            }
            else if (isJump)
            {
                animator.Play("JumpUp");
            }
            else if (isMoving)
            {
                animator.Play("Cycle");
            }
            else
            {
                animator.Play("Idle");
            }
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("JumpUp"))
        {
            animator.Play("InAir");
        }
    }
    public void SetMoving(bool _newIsMoving)
    {
        isMoving = _newIsMoving;
    }
    public void SetJumping(bool _newIsJumping)
    {
        isJump = _newIsJumping;
    }
    public void IsGrounded(bool _newIsGrounded)
    {
        isGrounded = _newIsGrounded;
    }
}

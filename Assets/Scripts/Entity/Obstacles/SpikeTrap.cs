using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Entity
{
    [SerializeField] private int damage = 1;
    enum TrapMode
    {
        Flicker,
        PressurePlate,
        TotalModes
    }
    //Timers for when trap is active
    [SerializeField] private float deactiveTime = 2;
    [SerializeField] private float activeTime = 2;
    private bool isActive;
    private float counter;
    private bool hasPreasurePlateTriggered;

    //Mode
    [SerializeField] private TrapMode trapMode;

    //animator
    Animator _animator;

    public override void Init()
    {
        hasInit = true;
        isActive = false;
        hasPreasurePlateTriggered = false;
        counter = 0;

        //get animator
        _animator = GetComponentInChildren<Animator>();
    }

    public override void HandleUpdate(float _distortTime)
    {
        switch (trapMode)
        {
            case TrapMode.Flicker:
                counter += _distortTime * Time.deltaTime;
                //activate if not active and hit active time
                if (!isActive && counter >= activeTime)
                {
                    //reset
                    counter = 0;
                    //activate
                    isActive = true;
                }
                //deactivate if active and hit deactivate time
                else if (isActive && counter >= deactiveTime)
                {
                    //reset
                    counter = 0;
                    //deactivate
                    isActive = false;
                }
                    break;
            case TrapMode.PressurePlate:
                //if preasure plate has been triggered
                if (hasPreasurePlateTriggered)
                {
                    counter += Time.deltaTime * _distortTime;
                    //activate
                    if (!isActive && counter >= activeTime)
                    {
                        isActive = true;
                        counter = 0;
                    }
                    else if (isActive && counter >= deactiveTime)
                    {
                        counter = 0;
                        isActive = false;
                        hasPreasurePlateTriggered = false;
                    }
                }
                break;
            default:
                break;
        }

        _animator.SetBool("ISACTIVE", isActive);
    }

    //check collision
    void OnTriggerStay2D(Collider2D other)
    {
        //check if preasure plate triggered
        if (trapMode == TrapMode.PressurePlate && other.gameObject.tag == "Player" && !hasPreasurePlateTriggered)
        {
            hasPreasurePlateTriggered = true;
        }

        //check if player and is active
        //check if is player
        if (isActive && other.gameObject.tag == "Player")
        {
            //deal damage
            other.gameObject.GetComponent<PlayerHealth>().AddHealth(-damage);
        }
    }
}

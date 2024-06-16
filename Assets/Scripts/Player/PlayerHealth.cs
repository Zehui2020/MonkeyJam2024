using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int maxHealth;
    private int currHealth;
    [SerializeField] float iframes;
    private float currIFrames;

    private EntityAudioController entityAudioController;

    private void Start()
    {
        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }
    }

    public void Initialise()
    {
        currIFrames = 0;
        currHealth = maxHealth;
    }
    public void UpdatePlayerHealth()
    {
        if (currIFrames > 0)
        {
            currIFrames -= Time.deltaTime;
        }
    }
    public void AddHealth(int _increment){
        if (currIFrames <= 0 || _increment > 0)
        {
            //check if heal
            if (_increment > 0)
            {
                //heal sound
                entityAudioController.PlayAudio("heal");
            }
            //check if damaged
            else
            {
                //damage sound
                entityAudioController.PlayAudio("playerhurt");
            }

            currHealth += _increment;
            if (currHealth <= 0)
            {
                Lose();
                currIFrames = iframes;
            }
        }
    }

    void Lose()
    {

    }
}

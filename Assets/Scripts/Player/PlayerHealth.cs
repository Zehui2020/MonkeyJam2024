using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int maxHealth;
    private int currHealth;
    [SerializeField] float iframes;
    [SerializeField] SpriteRenderer playerSprite;
    private float currIFrames;

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
            playerSprite.color = new Color(0.5f,0,0);
            if (currIFrames <= 0)
            {
                playerSprite.color = Color.white;
            }
        }
    }
    public void AddHealth(int _increment){
        if (currIFrames <= 0)
        {
            currHealth += _increment;
            if (currHealth <= 0)
            {
                Lose();
                
            }
            currIFrames = iframes;
        }
    }

    void Lose()
    {

    }
}

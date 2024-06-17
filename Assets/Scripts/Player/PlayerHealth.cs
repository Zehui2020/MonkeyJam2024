using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Sprite[] _sprites;
    public Image[] _images;

    [SerializeField] int maxHealth;
    private int currHealth;
    [SerializeField] float iframes;
    [SerializeField] SpriteRenderer playerSprite;
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
        HandleUI();
    }
    public void UpdatePlayerHealth()
    {
        if (currIFrames > 0)
        {
            currIFrames -= Time.deltaTime;
            playerSprite.color = new Color(1,0.5f,0.5f);
            if (currIFrames <= 0)
            {
                playerSprite.color = Color.white;
            }
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

            currHealth = (currHealth + _increment) % (maxHealth + 1);
            if (currHealth <= 0)
            {
                Lose();
                
            }
            HandleUI();
            currIFrames = iframes;
        }
    }

    //Handle UI
    void HandleUI()
    {
        int lostHealth = maxHealth - currHealth;
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < lostHealth)
            {
                //set black heart
                _images[i].sprite = _sprites[0];
            }
            else
            {
                //set red
                _images[i].sprite = _sprites[1];
            }
        }
    }

    void Lose()
    {
        SceneManagment.Instance.LoadScene("EndScreenLose");
    }
}

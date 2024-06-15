using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //check if is player
        if (collision.gameObject.tag == "Player")
        {
            //deal damage
            collision.gameObject.GetComponent<PlayerHealth>().AddHealth(-damage);
        }
    }
}

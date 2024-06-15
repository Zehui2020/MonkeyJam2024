using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMachine : Entity
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float fireRate = 5;
    private float counter;
    public override void Init()
    {
        hasInit = true;
        counter = 0;
    }

    public override void HandleUpdate(float _distortTime)
    {
        counter += Time.deltaTime * _distortTime;

        if (counter > fireRate) 
        {
            //reset
            counter = 0;
            //fire projectile
            GameObject new_obj = Instantiate(_projectile, transform);
            //to the right
            //transform.right;

        }
    } 
}

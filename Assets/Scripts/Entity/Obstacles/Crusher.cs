using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : Entity
{
    private float counter;

    public override void Init()
    {
        hasInit = true;
        counter = 0;
    }
    public override void HandleUpdate(float _distortTime)
    {
        
    }
}

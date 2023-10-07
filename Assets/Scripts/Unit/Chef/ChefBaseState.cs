using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public abstract class ChefBaseState : StateBase
{
    public Chef chef;
    void Awake()
    {
        chef = GetComponentInParent<Chef>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DishWasherBaseState : StateBase
{
    public DishWasher dishwasher;
    void Awake()
    {
        dishwasher = GetComponentInParent<DishWasher>();           
    }
}

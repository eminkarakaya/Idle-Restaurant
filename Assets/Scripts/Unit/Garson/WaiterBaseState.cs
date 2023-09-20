using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaiterBaseState : StateBase
{
    public Waiter waiter;
    void Awake()
    {
        waiter = GetComponentInParent<Waiter>();
    }
}

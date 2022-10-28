using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GarsonState : StateBase
{
    public Garson waiter;
    void Awake()
    {
        waiter = GetComponentInParent<Garson>();
    }
}

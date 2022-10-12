using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GarsonState : StateBase
{
    public Garson garson;
    void Awake()
    {
        garson = GetComponentInParent<Garson>();
    }
}

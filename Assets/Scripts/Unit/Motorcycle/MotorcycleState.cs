using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotorcycleState : StateBase
{
    public Motorcycle motorcycle;
    void Awake()
    {
        motorcycle = GetComponentInParent<Motorcycle>();
    }
    void Start()
    {
        item = motorcycle.parkinLot.sira;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomerBaseState : StateBase
{
    public Customer customer;
    void Awake()
    {
        item = Door.instance;
        customer = GetComponentInParent<Customer>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MusteriState : StateBase
{
    public Musteri customer;
    void Awake()
    {
        item = kapi.instance;
        customer = GetComponentInParent<Musteri>();
    }
}

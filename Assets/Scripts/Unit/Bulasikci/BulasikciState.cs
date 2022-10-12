using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulasikciState : StateBase
{
    public Bulasikci bulasikci;
    void Awake()
    {
        bulasikci = GetComponentInParent<Bulasikci>();           
    }
}

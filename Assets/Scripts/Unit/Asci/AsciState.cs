using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AsciState : StateBase
{
    public Asci asci;
    void Awake()
    {
        asci = GetComponentInParent<Asci>();
    }
}

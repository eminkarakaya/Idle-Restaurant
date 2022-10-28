using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AsciState : StateBase
{
    public Asci chef;
    void Awake()
    {
        chef = GetComponentInParent<Asci>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MusteriState : StateBase
{
    public Musteri musteri;
    void Awake()
    {
        item = kapi.instance;
        musteri = GetComponentInParent<Musteri>();
    }
}

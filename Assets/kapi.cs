using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kapi : Item
{
    public Transform kapiTransform;
    public static kapi instance;
    void Awake()
    {
        instance = this;
        kapiTransform = transform;
    }
}

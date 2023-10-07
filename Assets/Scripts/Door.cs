using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
    public Transform doorTransform;
    public static Door instance;
    void Awake()
    {
        instance = this;
        doorTransform = transform;
    }
}

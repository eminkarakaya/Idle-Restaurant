using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
    public Transform kapiTransform;
    public static Door instance;
    void Awake()
    {
        instance = this;
        kapiTransform = transform;
    }
}

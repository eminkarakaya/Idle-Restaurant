using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Item
{
    public GameObject engel;
    public bool isFull;
    public Transform garsonPos;
    public GameObject food;
    void Awake()
    {
        // engel = transform.GetChild(3).gameObject;
        // tabakYeri = transform.GetChild(0);
        // asciYeri = transform.GetChild(1);
        // garsonPos = transform.GetChild(2);
    }
}

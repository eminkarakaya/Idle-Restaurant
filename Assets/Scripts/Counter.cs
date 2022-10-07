using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public List<Asci> asci;
    public GameObject engel;
    public Transform platePos;
    public bool isFull;
    public Transform garsonPos;
    public Transform asciPos;
    public GameObject food;
    void Awake()
    {
        engel = transform.GetChild(3).gameObject;
        platePos = transform.GetChild(0);
        asciPos = transform.GetChild(1);
        garsonPos = transform.GetChild(2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Department : MonoBehaviour
{
    public GameObject _lock;
    public GameObject lockedPanel;
    public bool isLocked;
    public abstract Level level {get; set;}
    public abstract GameObject acilacakPanel { get; set; }
    public abstract Transform camPlace { get; set; }
    public abstract Transform oldCamPlace { get; set; }
    public Collider selectableCollider;
    void Start()
    {
        selectableCollider = GetComponent<Collider>();
    }
}

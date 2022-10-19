using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Department : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject _lock;
    public GameObject lockedPanel;
    public bool isLocked;
    public abstract Level level {get; set;}
    public abstract GameObject acilacakPanel { get; set; }
    public abstract Transform camPlace { get; set; }
    public Transform oldCamPlace;
    public Collider selectableCollider;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        selectableCollider = GetComponent<Collider>();
    }
}

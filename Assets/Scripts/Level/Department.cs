using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Department : MonoBehaviour
{
    [Header("Lock Panel")]
    public Gold unlockCost;
    [HideInInspector] public LevelManager levelManager;
    public GameObject @lock;
    public GameObject lockedPanel;
    public bool isLocked;
    public abstract Level level {get; set;}
    public abstract GameObject dataPanel { get; set; }
    public abstract Transform camPlace { get; set; }
    [HideInInspector] public Transform oldCamPlace;
    [HideInInspector] public Collider selectableCollider;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        selectableCollider = GetComponent<Collider>();
    }
}

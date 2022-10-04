using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : Department 
{
    public override GameObject acilacakPanel { get; set; }
    public override Transform camPlace { get; set; }
    public override Transform oldCamPlace { get; set; }
    public override Collider selectableCollider { get; set; }
    [SerializeField] private Transform _camTransform;
    [SerializeField] private GameObject _acilacakPanel;
    void Start()
    {
        camPlace = _camTransform;
        selectableCollider = GetComponent<Collider>();
        acilacakPanel = _acilacakPanel;
    }
}

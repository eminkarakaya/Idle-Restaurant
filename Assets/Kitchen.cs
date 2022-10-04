using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : Department
{
    [SerializeField] private Transform _camTransform;
    [SerializeField] private GameObject _acilacakPanel;
    public override GameObject acilacakPanel { get; set; }
    [SerializeField] public override Transform camPlace { get; set; }
    public override Collider selectableCollider { get; set; }
    public override Transform oldCamPlace { get; set; }

    void Start()
    {
        camPlace = _camTransform;
        selectableCollider = GetComponent<Collider>();
        acilacakPanel = _acilacakPanel;
    }
}

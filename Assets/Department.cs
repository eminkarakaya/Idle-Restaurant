using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Department : MonoBehaviour
{
    public abstract GameObject acilacakPanel { get; set; }
    public abstract Transform camPlace { get; set; }
    public abstract Transform oldCamPlace { get; set; }
    public abstract Collider selectableCollider { get; set; }
}

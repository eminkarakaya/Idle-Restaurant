using UnityEngine;
public interface ISelectable
{
    public GameObject acilacakPanel { get; set; }
    public Transform camPlace { get; set; }
    public Transform oldCamPlace { get; set; }
    public Collider selectableCollider { get; set; }
}

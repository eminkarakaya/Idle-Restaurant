using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    public Item item;
    // protected Asci asci;
    // void Awake()
    // {
    //     asci = GetComponentInParent<Asci>();
    // }
    public abstract void StartState(Action action);
    public abstract void UpdateState(Action action);
}

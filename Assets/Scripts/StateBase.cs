using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    public Item item;
    public abstract void StartState(Action action);
    public abstract void UpdateState(Action action);
}

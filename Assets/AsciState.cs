using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AsciState : MonoBehaviour
{
    public abstract void StartState(Action action);
    public abstract AsciState UpdateState(Action action);
}

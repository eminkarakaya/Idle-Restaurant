using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GarsonState : MonoBehaviour
{
    
    public abstract void StartState(Action action);
    public abstract GarsonState UpdateState(Action action);
}

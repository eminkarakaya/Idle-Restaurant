using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GarsonState
{
    Garson garson;
    void Start()
    {
        garson = GetComponentInParent<Garson>();
        garson.action.Dur();
    }
    public override void StartState(Action action)
    {
        Debug.Log("startstate " + this.name);
    }
    public override GarsonState UpdateState(Action action)
    {
        
        if(FindCounter())
        {
            return garson.walkState;
        }
        return this;
    }
    public bool FindCounter()
    {
        var counter = garson.FindCounter();
        if(counter == null)
        {
            return false;
        }
        return true;
    }
}

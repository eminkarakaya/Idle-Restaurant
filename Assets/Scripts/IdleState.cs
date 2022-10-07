using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GarsonState
{
    Garson garson;
    void Start()
    {
    }
    public override void StartState(Action action)
    {
        garson = GetComponentInParent<Garson>();
        garson.action.Dur();
    }
    public override void UpdateState(Action action)
    {
        if(FindCounter())
        {
            garson.currentState = garson.walkState;
        }
        return;
    }
    public bool FindCounter()
    {
        var counter = garson.FindCounter();
        Debug.Log( "counter " + counter + " garson " + garson);
        if(counter == null)
        {
            return false;
        }
        // garson.counter = counter;
        if(!counter.isFull)
            return false;
        // garson.level.yemegiHazirCounterler.Remove(counter);
        return true;
    }
}

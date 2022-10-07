using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : GarsonState
{   
    Garson garson;
    public override void StartState(Action action)
    {
        action.Yuru();
        garson = GetComponentInParent<Garson>();
        if(garson.counter == null)
        {
            garson.agent.SetDestination(garson.beklemeYeri.position);
        }
        else
            garson.agent.SetDestination(garson.counter.garsonPos.position);
    }
    public override void UpdateState(Action action)
    {
        if(garson.counter != null)
        {
            if(Vector3.Distance(garson.agent.transform.position,garson.counter.transform.position) < 0.5f)
            {
                garson.currentState = garson.tasiState;
                return;
            }
        }
        var counter = garson.FindCounter();
        if(garson.counter == null)
        {
            if(Vector3.Distance(garson.agent.transform.position,garson.beklemeYeri.position) < 0.3f)
            {
                garson.currentState = garson.idleState;
                return;
            }
            return;
        }
        // garson.agent.SetDestination(garson.counter.garsonPos.position);
        
        garson.currentState = this;
    }
}

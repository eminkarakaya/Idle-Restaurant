using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YemegiCounterdenAlState : GarsonState
{
    public override void StartState(Action action)
    {
        action.Yuru();
    }
    public override void UpdateState(Action action)
    {
        if(garson.counter != null)
        {
        garson.agent.SetDestination(garson.counter.garsonPos.position);
            if(Vector3.Distance(garson.agent.transform.position,garson.counter.transform.position) < 0.5f)
            {
                garson.currState = garson.tasiState;
                return;
            }
            return;
        }
        var counter = garson.FindCounter();
        if(garson.counter == null)
        {
            if(Vector3.Distance(garson.agent.transform.position,garson.beklemeYeri.position) < 0.3f)
            {
                garson.currState = garson.idleState;
                return;
            }
            return;
        }
        garson.agent.SetDestination(garson.counter.garsonPos.position);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YemegiCounterdenAlState : GarsonState
{
    public override void StartState(Action action)
    {
        action.Walk();
    }
    public override void UpdateState(Action action)
    {
        if(waiter.counter != null)
        {
        waiter.agent.SetDestination(waiter.counter.waiterPos.position);
            if(Vector3.Distance(waiter.agent.transform.position,waiter.counter.transform.position) < 0.5f)
            {
                waiter.currState = waiter.tasiState;
                return;
            }
            return;
        }
        var counter = waiter.FindCounter();
        if(waiter.counter == null)
        {
            if(Vector3.Distance(waiter.agent.transform.position,waiter.waitingPlace.position) < 0.3f)
            {
                waiter.currState = waiter.idleState;
                return;
            }
            return;
        }
        waiter.agent.SetDestination(waiter.counter.waiterPos.position);
    }

}

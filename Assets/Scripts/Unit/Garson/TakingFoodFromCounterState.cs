using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingFoodFromCounterState : WaiterBaseState
{

    public Counter counter;
    public override void StartState(Action action)
    {
        action.Walk();
    }
    public override void UpdateState(Action action)
    {
        if(counter != null)
        {
            waiter.agent.SetDestination(counter.waiterPos.position);
            if(Vector3.Distance(waiter.agent.transform.position,counter.transform.position) < 0.8f)
            {
                waiter.tasiState.waiter.counter = counter;
                counter = null;
                waiter.currState = waiter.tasiState;
                return;
            }
            return;
        }
        counter = waiter.FindCounter();
        if(counter == null)
        {
            if(Vector3.Distance(waiter.agent.transform.position,waiter.waitingPlace.position) < 0.5f)
            {
                waiter.currState = waiter.beklemeYerineGitState;
                return;
            }
            return;
        }
        waiter.agent.SetDestination(counter.waiterPos.position);
    }

}

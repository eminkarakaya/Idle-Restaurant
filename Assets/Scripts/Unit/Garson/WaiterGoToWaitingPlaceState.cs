using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterGoToWaitingPlaceState : WaiterBaseState
{
    public override void StartState(Action action)
    {
        action.Walk();
        waiter.agent.SetDestination(waiter.waitingPlace.position);
    }
    public override void UpdateState(Action action)
    {
        if(waiter.targetKirli == null && waiter.level.restaurant.dirtyPlates.Count !=0)
            waiter.targetKirli = waiter.level.restaurant.dirtyPlates[0];
        if(waiter.targetKirli != null)
        {
            waiter.currState = waiter.bulasikToplaState;
            return;
        }
        if(waiter.chair == null)
        {
            waiter.chair = waiter.FindChair();
        }
        if(waiter.chair != null)
        {
            waiter.tasiState.waiter.chair = waiter.chair;
            waiter.currState = waiter.yemegiCounterdenAlState;
            return;
        }
        if(Vector3.Distance(waiter.transform.position, waiter.waitingPlace.transform.position)<.4f)
        {
            waiter.currState = waiter.idleState;
        }
    }
}

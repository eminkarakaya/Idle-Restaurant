using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterGoToWaitingPlaceState : WaiterBaseState
{
    public override void StartState(Action action)
    {
        action.Walk();
        waiter.agent.SetDestination(waiter.waitingPlace.position);
        if(!waiter.restaurant.availableWaiters.Contains(waiter))
        {
            waiter.restaurant.availableWaiters.Add(waiter);
        }
        if(waiter.CheckDeliver())
        {
            return;
        }


        waiter.CheckDirtyPlate();
    }
    public override void UpdateState(Action action)
    {
        // if(waiter.targetKirli == null && waiter.level.restaurant.dirtyPlates.Count !=0)
        //     waiter.targetKirli = waiter.level.restaurant.dirtyPlates[0];
        // if(waiter.targetKirli != null)
        // {
        //     if(waiter.restaurant.availableWaiters.Contains(waiter))
        //     {
        //         waiter.restaurant.availableWaiters.Remove(waiter);
        //     }
        //     waiter.currState = waiter.bulasikToplaState;
        //     return;
        // }
      
        if(Vector3.Distance(waiter.transform.position, waiter.waitingPlace.transform.position)<.4f)
        {
            waiter.currState = waiter.idleState;
        }
    }
}

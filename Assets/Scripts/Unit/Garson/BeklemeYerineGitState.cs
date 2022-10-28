using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeklemeYerineGitState : GarsonState
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
        if(waiter._chair == null)
        {
            waiter._chair = waiter.FindChair();
        }
        if(waiter._chair != null)
        {
            waiter.currState = waiter.yemegiCounterdenAlState;
            return;
        }
        if(Vector3.Distance(waiter.transform.position, waiter.waitingPlace.transform.position)<.4f)
        {
            waiter.currState = waiter.idleState;
        }
    }
}

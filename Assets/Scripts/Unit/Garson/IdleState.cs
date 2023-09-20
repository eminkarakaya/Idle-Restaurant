using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : WaiterBaseState
{
    public override void StartState(Action action)
    {
        waiter.queueImage.gameObject.SetActive(true);
        waiter.action.Idle();
    }
    public override void UpdateState(Action action)
    {
        if(waiter.targetKirli == null && waiter.level.restaurant.dirtyPlates.Count !=0)
            waiter.targetKirli = waiter.level.restaurant.dirtyPlates[0];
        if(waiter.targetKirli != null)
        {
            waiter.queueImage.gameObject.SetActive(false);
            waiter.currState = waiter.bulasikToplaState;
            return;
        }
        if(waiter.chair == null)
        {
            waiter.chair = waiter.FindChair();
        }
        if(waiter.counter == null)
        {
            waiter.counter = waiter.FindCounter();
        }
        
        if(waiter.chair != null && waiter.counter != null)
        {
            waiter.queueImage.gameObject.SetActive(false);
            waiter.tasiState.waiter.chair = waiter.chair;
            waiter.yemegiCounterdenAlState.counter = waiter.counter;
            waiter.counter = null;
            waiter.currState = waiter.yemegiCounterdenAlState;
            return;
        }
    }    
}

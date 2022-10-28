using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GarsonState
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
        if(waiter._chair == null)
        {
            waiter._chair = waiter.FindChair();
        }
        if(waiter.counter == null)
        {
            var counter = waiter.FindCounter();
        }
        
        if(waiter._chair != null && waiter.counter != null)
        {
            waiter.queueImage.gameObject.SetActive(false);
            waiter.currState = waiter.yemegiCounterdenAlState;
            return;
        }
    }    
}

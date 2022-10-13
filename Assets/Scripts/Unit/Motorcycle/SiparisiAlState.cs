using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiparisiAlState : MotorcycleState
{
    public override void StartState(Action action)
    {
        item = motorcycle.parkinLot.sira;
        var counter = motorcycle.FindCounter();
        motorcycle.counter = counter;
        if(!item.queue.Contains(motorcycle))
            item.CreateQueue(motorcycle);
        if(counter == null)
        {
            motorcycle.siparisiBekleState.item = item;
            motorcycle.queueState.oncekiState = motorcycle.siparisiBekleState;
            motorcycle.currState = motorcycle.queueState;
            return;
        }
        motorcycle.agent.SetDestination(counter.garsonPos.position);
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(motorcycle.transform.position,motorcycle.counter.garsonPos.position) < 1)
        {
            motorcycle.currState = motorcycle.siparisiBekleState;
        }
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountereKoymaState : AsciState
{
    public override void StartState(Action action)
    {
        item = asci.counter;
        
        item.CreateQueue(asci);
        if(item.queue[0] != asci)
        {
            asci.queueState.oncekiState = asci.currState;
            // asci.queueState.oncekiAction = action.;
            
            asci.currState = asci.queueState;
        }
        // pizza = asci.pizza;
        action.Tasi();
    }
    public override void UpdateState(Action action)
    {
        
        asci.agent.SetDestination(item.asciYeri.position);
        
        if(Vector3.Distance(asci.agent.transform.position,asci.counter.asciYeri.position) > .4f)
        {
            return;
        }
            asci.currState = asci.counterFullState;
        if(asci.counter.isFull)
        {
            asci.currState = asci.counterFullState;
            return;
        }
        // if(counter.isFull)
        // {
        //     asci.counterFullState.pizza = pizza;
        //     asci.counterFullState.counter = this.counter;
        //     asci.currState = asci.counterFullState;
        // }
        
    }
}

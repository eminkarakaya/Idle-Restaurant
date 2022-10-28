using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountereKoymaState : AsciState
{

    public override void StartState(Action action)
    {
        item = chef.counter;
        
        item.CreateQueue(chef);
        if(item.queue[0] != chef)
        {
            
            chef.queueState.isCarrying = true;            
            chef.queueState.previousState = chef.currState;            
            chef.currState = chef.queueState;
        }
        action.Carry();
        
        StartCoroutine(GameManager.instance.SetDestinationCouroutine(item.chefPlace.position,chef,this));
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(chef.agent.transform.position,chef.counter.chefPlace.position) > .4f)
        {
            return;
        }
            chef.currState = chef.counterFullState;
        if(chef.counter.isFull)
        {
            chef.currState = chef.counterFullState;
            return;
        }
        
    }
}

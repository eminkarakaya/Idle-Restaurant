using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirinaKoymaState : AsciState
{
    Transform firinTransform;
    float offset = .3f;
    public override void StartState(Action action)
    {
        item = chef.oven;
        firinTransform = item.chefPlace.transform;
        if(!item.queue.Contains(chef))
            item.CreateQueue(chef);
        
        if(item.queue[0] != chef)//&& item.queue[0] != asci)
        {
            chef.queueState.isCarrying = true;
            chef.queueState.previousState = chef.currState;
            chef.currState = chef.queueState;
        }
        chef.agent.SetDestination(firinTransform.position);
        action.Carry();
        
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(chef.agent.transform.position,firinTransform.position) > offset)
        {
            return;
        }
        chef.pizza.transform.SetParent(null);
        chef.pizza.transform.position = item.platePlaces[0].position;
        chef.pizza.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
        // asci.pizza = null;
        
        chef.currState = chef.waitForOvenState;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishwasherTakingPlateState : DishWasherBaseState
{
    public override void StartState(Action action)
    {   
        item = dishwasher.dishCounter;
        item.CreateQueue(dishwasher);
        if(item.queue[0] != dishwasher)
        {
            dishwasher.queueState.previousState = dishwasher.waitPlateState;
            dishwasher.queueState.isCarrying = false;
            dishwasher.queueState.previousState.item = item;
            dishwasher.currState = dishwasher.queueState;
        }
        action.Walk();
    }
    public override void UpdateState(Action action)
    {
        dishwasher.agent.SetDestination(dishwasher.dishCounter.chefPlace.position);
        if(Vector3.Distance(dishwasher.agent.transform.position,dishwasher.dishCounter.chefPlace.position) < .4f)
        {
            dishwasher.currState = dishwasher.waitPlateState;   
        }
    }
}

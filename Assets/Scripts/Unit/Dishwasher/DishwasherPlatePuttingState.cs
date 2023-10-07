using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishwasherPlatePuttingState : DishWasherBaseState
{
    // public Sink FindSink()
    // {
    //     List<Sink> allSinks = dishwasher.scullery.currentSinks;
    //     var enaz = allSinks[0];
    //     for (int i = 0; i < allSinks.Count; i++)
    //     {
    //         if(allSinks[i].dishwashers.Count < enaz.dishwashers.Count)
    //         {
    //             enaz = allSinks[i];
    //         }
    //     }
    //     dishwasher.sink = enaz;
    //     return enaz;
    // }

    public override void StartState(Action action)
    {
        action.Carry();
        
        item = dishwasher.sink;
        item.CreateQueue(dishwasher);
        if(item.queue[0] != dishwasher)
        {
            dishwasher.waitSinkState.item = item;
            dishwasher.queueState.isCarrying = true;
            dishwasher.queueState.previousState = dishwasher.waitSinkState;
            dishwasher.currState = dishwasher.queueState;
        }

        dishwasher.agent.SetDestination(dishwasher.sink.chefPlace.position);
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(dishwasher.transform.position,dishwasher.sink.dishwasherPlace.transform.position) < .6f)
        {
            dishwasher.currState = dishwasher.washState;
        }
    }
}

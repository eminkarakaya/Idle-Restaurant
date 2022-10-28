using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulasikGoturState : GarsonState
{
    public Tabak tabak;
    public BulasikCounter bulasikCounter;
    public override void StartState(Action action)
    {
        action.Carry();
        for (int i = 0; i < waiter.level.scullery.Count; i++)
        {
            bulasikCounter = waiter.level.scullery[i].FindDishCounter();
        }
        item = bulasikCounter.waiterItem;
        item.CreateQueue(waiter);

        if(item.queue[0] != waiter)
        {
            waiter.queueState.isCarrying = true;
            waiter.bulasikGoturIdle.tabak = tabak;
            waiter.bulasikGoturIdle.item = bulasikCounter.waiterItem;
            waiter.bulasikGoturIdle.bulasikCounter = bulasikCounter;
            waiter.queueState.previousState = waiter.bulasikGoturIdle;
            waiter.currState = waiter.queueState;
        }
        waiter.agent.SetDestination(bulasikCounter.dishwasherPlace.position);
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(waiter.transform.position,bulasikCounter.dishwasherPlace.position) < .5f || bulasikCounter.plates.Count > 0)
        {
            waiter.bulasikGoturIdle.tabak = tabak;
            waiter.bulasikGoturIdle.bulasikCounter = bulasikCounter;
            waiter.currState = waiter.bulasikGoturIdle;
        }
    }
}

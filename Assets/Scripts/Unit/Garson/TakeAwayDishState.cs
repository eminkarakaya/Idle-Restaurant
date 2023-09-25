using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAwayDishState : WaiterBaseState
{
    public Plate tabak;
    public SculleryCounter bulasikCounter;

    public override void StartState(Action action)
    {
        action.Carry();
        List<Scullery> sculleriesToBeRemoved = new List<Scullery>();
        List<SculleryCounter> sculleryCountersToBeRemoved = new List<SculleryCounter>();
        Scullery scullery = FindNearestScullery(waiter.transform.position);
        bulasikCounter = scullery.FindDishCounterForWaiters(waiter.transform.position);
        while(bulasikCounter.CheckQueueCapacityIsFull())
        {   
            sculleryCountersToBeRemoved.Add(bulasikCounter);
            bulasikCounter = scullery.FindDishCounterForWaiters(waiter.transform.position,sculleryCountersToBeRemoved);
            if(bulasikCounter == null)
            {
                sculleriesToBeRemoved.Add(scullery);
                scullery = FindNearestScullery(waiter.transform.position,sculleriesToBeRemoved);
                bulasikCounter = scullery.FindDishCounterForWaiters(waiter.transform.position,sculleryCountersToBeRemoved);
            }
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
        if(Vector3.Distance(waiter.transform.position,bulasikCounter.waiterItem.chefPlace.position) < .6f ) // || bulasikCounter.plates.Count > 0
        {
            waiter.bulasikGoturIdle.tabak = tabak;
            waiter.bulasikGoturIdle.bulasikCounter = bulasikCounter;
            waiter.currState = waiter.bulasikGoturIdle;
        }
    }

    public Scullery FindNearestScullery(Vector3 pos , List<Scullery> sculleries = null)
    {
        List<Scullery> tempSculleries = new List<Scullery>();
        for (int i = 0; i < waiter.level.unlockedSculleries.Count; i++)
        {
            tempSculleries.Add (waiter.level.unlockedSculleries[i]);
        }
        if(sculleries != null)
        {
            for (int i = 0; i < sculleries.Count; i++)
            {
                tempSculleries.Remove(sculleries[i]);
            }
        }
        if(tempSculleries.Count == 0) return null;
        Scullery nearestScullery = tempSculleries[0];
        float nearestDistance = Vector3.Distance(pos,nearestScullery.transform.position);
        for (int i = 0; i < tempSculleries.Count; i++)
        {
            float newDistance = Vector3.Distance(pos,tempSculleries[i].transform.position);
            if(newDistance < nearestDistance)
            {
                nearestDistance = newDistance;
                nearestScullery = tempSculleries[i]; 
            }
        }
        return nearestScullery;
    }
  

}


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TakeAwayDishState : WaiterBaseState
{
    
    
    public Plate tabak;
    public SculleryCounter bulasikCounter;

    public override void StartState(Action action)
    {
        action.Carry();
        List<SculleryCounter> sculleryCountersToBeRemoved = new List<SculleryCounter>();
        bulasikCounter = FindDishCounterForWaiters(waiter.transform.position);
        while(bulasikCounter.CheckQueueCapacityIsFull())
        {   
            sculleryCountersToBeRemoved.Add(bulasikCounter);
            bulasikCounter = FindDishCounterForWaiters(waiter.transform.position,sculleryCountersToBeRemoved);
            if(bulasikCounter == null)
            {
                bulasikCounter = FindDishCounterForWaiters(waiter.transform.position,sculleryCountersToBeRemoved);
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
    private Dictionary<SculleryCounter,float> PrepareDict(List<SculleryCounter> sculleryCounters)
    {
        Dictionary<SculleryCounter,float> CounterAndMoveTimeDict = new Dictionary<SculleryCounter, float>();
        foreach (var item in sculleryCounters)
        {
            float time = Vector3.Distance(item.transform.position,waiter.transform.position)/waiter.moveSpeed;
            CounterAndMoveTimeDict.Add(item,time);
        }
        return CounterAndMoveTimeDict;
    }
    private SculleryCounter FindNearestCounter(List<SculleryCounter> sculleryCounters)
    {
        SculleryCounter leastCounter = null;
        Dictionary<SculleryCounter,float> counterAndMoveTimeDict = PrepareDict(sculleryCounters);
        float prevWashingTimeXqueue = 0f;
        float prevMoveTime = 0f;
        foreach (var item in counterAndMoveTimeDict.OrderBy(x=>x.Value))
        {
            if(prevWashingTimeXqueue == 0)
            {
                leastCounter = item.Key;
                prevMoveTime = item.Value;
                prevWashingTimeXqueue = item.Key.scullery.washingTime*(item.Key.waiterItem.queue.Count+1);
                continue;
            }
            if(prevWashingTimeXqueue + prevMoveTime > item.Value + (1+item.Key.waiterItem.queue.Count) *item.Key.scullery.washingTime)
            {
                leastCounter = item.Key;
                prevWashingTimeXqueue = item.Key.scullery.washingTime*(item.Key.waiterItem.queue.Count+1);
                prevMoveTime = item.Value;
            }
        }
        return leastCounter;
    }
  
    public SculleryCounter FindDishCounterForWaiters(Vector3 pos,List<SculleryCounter> sculleryCounters = null)
    {
        List<SculleryCounter> tempDishCounters = new List<SculleryCounter>();
        foreach (Scullery scullery in waiter.level.unlockedSculleries)
        {
            foreach (var item in scullery.currentDishCounters)
            {
                if(item.dishwashers.Count == 0)
                    continue;
                tempDishCounters.Add(item);
            }
        }
        if(sculleryCounters != null)
        {
            for (int i = 0; i < sculleryCounters.Count; i++)
            {
                tempDishCounters.Remove(sculleryCounters[i]);
            }
        }
        if(tempDishCounters.Count == 0) 
            return null;
        
        return FindNearestCounter(tempDishCounters);
    }
}

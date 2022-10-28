using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiparisiBekleState : MotorcycleState
{
    public override void StartState(Action action)
    {
        item = motorcycle.parkinLot.queue;
    }
    public override void UpdateState(Action action)
    {
        if(motorcycle.counter != null)
        {
            motorcycle.counter.food.transform.SetParent(motorcycle.transform);
            motorcycle.pizza = motorcycle.counter.food;
            motorcycle.counter.food.transform.position = motorcycle.tabakPos.position;
            motorcycle.counter.isFull = false;
            item.UpdateQueue(motorcycle);
            motorcycle.currState = motorcycle.siparisiGoturState;
        }
        var counter = motorcycle.FindCounter();
    }

}

    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterFullState : ChefBaseState
{
    public bool kuryeAscisimi;
    float timeOffset = .3f;
    float timeOffsetTemp;
    public override void StartState(Action action)
    {
        chef.queueImage.gameObject.SetActive(true);
        action.WaitWithFood();
    }
    public override void UpdateState(Action action)
    {
        if(!chef.counter.isFull)
        {
            timeOffsetTemp += Time.deltaTime;
            if(timeOffsetTemp < timeOffset)
            {
                return;
            }
            timeOffsetTemp = 0;
            chef.pizza.transform.SetParent(null);
            chef.pizza.transform.position = chef.counter.platePlaces[0].position;
            chef.counter.food = chef.pizza;
            chef.counter.isFull = true;
            if(kuryeAscisimi)
            {
                chef.kitchen.parkinLot.foodReadyCounters.Add(chef.counter);
            }
            else
            {
                chef.level.restaurant.foodReadyCounters.Add(chef.counter);
            }
            
            chef.pizza.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
            chef.counter.UpdateQueue(chef);
            chef.queueImage.gameObject.SetActive(false);
            chef.currState = chef.fridgeState;
        }
    }
}

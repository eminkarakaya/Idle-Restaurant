using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishwasherWashState : DishWasherBaseState
{
    [HideInInspector] public Plate plate;
    Scullery scullery;
    float washingTimeTemp;
    private void Start() {
        scullery = dishwasher.scullery;
    }
    public override void StartState(Action action)
    {  
        action.Idle();
        plate.transform.SetParent(null);
        plate.transform.position = dishwasher.sink.platePlaces[0].position;
        dishwasher.slider.gameObject.SetActive(true);
        dishwasher.slider.maxValue = scullery.washingTime;
        dishwasher.slider.value = 0;
    }
    public override void UpdateState(Action action)
    {
        washingTimeTemp += Time.deltaTime;
        dishwasher.slider.value = washingTimeTemp;
        if(washingTimeTemp > scullery.washingTime)
        {
            ObjectPool.instance.pools[1].pooledObjects.Enqueue(plate.gameObject);
            washingTimeTemp = 0;
            dishwasher.sink.UpdateQueue(dishwasher);
            dishwasher.slider.gameObject.SetActive(false);
            dishwasher.currState = dishwasher.takePlateState;
        }
    }
}

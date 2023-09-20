using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishwasherWaitingForPlateState : DishWasherBaseState
{
    public override void StartState(Action action)
    {
        item = dishwasher.dishCounter;
        action.Idle();
        dishwasher.queueImage.gameObject.SetActive(true);
    }
    public override void UpdateState(Action action)
    {
        if(dishwasher.dishCounter.plates.Count != 0)
        {
            dishwasher.queueImage.gameObject.SetActive(false);
            dishwasher.dishCounter.plates[dishwasher.dishCounter.plates.Count-1].transform.SetParent(dishwasher.transform);
            dishwasher.dishCounter.plates[dishwasher.dishCounter.plates.Count-1].transform.position = dishwasher.hand.position;
            dishwasher.dishCounter.plates[dishwasher.dishCounter.plates.Count-1].transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
            dishwasher.washState.plate = dishwasher.dishCounter.plates[dishwasher.dishCounter.plates.Count-1];
            dishwasher.dishCounter.plates.RemoveAt(dishwasher.dishCounter.plates.Count-1);
            dishwasher.currState = dishwasher.putPlateState;
            dishwasher.dishCounter.UpdateQueue(dishwasher);
        }
    }
}

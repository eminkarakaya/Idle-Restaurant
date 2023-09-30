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
        dishwasher.CheckCounter();
    }
    public override void UpdateState(Action action)
    {
        
    }
    
}

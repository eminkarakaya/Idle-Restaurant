using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkBekleState : BulasikciState
{
    public override void StartState(Action action)
    {
        action.Idle();
        dishwasher.queueImage.gameObject.SetActive(true);
    }
    public override void UpdateState(Action action)
    {
        if(!dishwasher.sink.isFull)
        {
            dishwasher.queueImage.gameObject.SetActive(false);
            // bulasikci.bulasikCounter.UpdateQueue(bulasikci);
            dishwasher.currState = dishwasher.putPlateState;
        }
    }
}

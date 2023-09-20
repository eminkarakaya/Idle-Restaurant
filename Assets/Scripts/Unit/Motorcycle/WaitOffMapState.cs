using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitOffMapState : MotorcycleState
{
    public float time;
    public float timeTemp = 0;
    
    public override void StartState(Action action)
    {
        
    }
    public override void UpdateState(Action action)
    {
        timeTemp += Time.deltaTime;
        if(timeTemp > time)
        {
            Destroy(motorcycle.pizza);
            timeTemp = 0;
            motorcycle.transform.position = motorcycle.parkinLot.startingPos.position;
            motorcycle.currState = motorcycle.siparisiAlState;
        }
    }
}

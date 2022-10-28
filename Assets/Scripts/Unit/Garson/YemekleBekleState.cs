using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YemekleBekleState : GarsonState
{
    void Start()
    {
        waiter = GetComponentInParent<Garson>();
    }
    public override void StartState(Action action)
    {
        action.WaitWithFood();
    }
    public override void UpdateState(Action action)
    {
        var chair = waiter.FindChair();
        if(chair != null)
        {
            waiter.currState =  waiter.tasiState;
        }
        return;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : WaiterBaseState
{
    public override void StartState(Action action)
    {
        waiter.queueImage.gameObject.SetActive(true);
        waiter.action.Idle();
    }
    
    public override void UpdateState(Action action)
    {
        
    }    
}

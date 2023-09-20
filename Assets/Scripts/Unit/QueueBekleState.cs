using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueBekleState : StateBase
{
    public bool isCarrying;
    public Action previousAction;
    public StateBase previousState;
    Unit unit;
    public override void StartState(Action action)
    {
        unit = GetComponentInParent<Unit>();
        unit.transform.LookAt(item.transform);
        if(unit.TryGetComponent(out Customer customer))
        {
            action.CustomerStandIdle();
        }
        else
        {
            if(isCarrying)
            {
                action.WaitWithFood();
            }
            else
            {
                action.Idle();
            }
        }
    }
    public override void UpdateState(Action action)
    {
        if(item.queue[0] == unit)
        {
            unit.queueImage.gameObject.SetActive(false);
            unit.currState = previousState;
        }
    }
}

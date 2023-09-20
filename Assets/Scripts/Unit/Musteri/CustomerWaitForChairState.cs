using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaitForChairState : CustomerBaseState
{
    public override void StartState(Action action)
    {

    }
    public override void UpdateState(Action action)
    {
        customer.chair = customer.FindEmptyChair();
        if(customer.chair != null)
        {
            customer.currState = customer.customerWalkState;
        }
    }
}

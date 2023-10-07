using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEatingState : CustomerBaseState
{
    public override void StartState(Action action)
    {
        action.CustomerEat();
        customer.orderImage.gameObject.SetActive(false);
    }
    public override void UpdateState(Action action)
    {
        
    }
    
}

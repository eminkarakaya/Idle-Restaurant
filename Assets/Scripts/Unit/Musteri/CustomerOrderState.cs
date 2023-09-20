using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderState : CustomerBaseState
{
    
    public override void StartState(Action action)
    {
        action.CustomerOrder();
        customer.orderImage.gameObject.SetActive(true);
    }
    public override void UpdateState(Action action)
    {
        
    }
}

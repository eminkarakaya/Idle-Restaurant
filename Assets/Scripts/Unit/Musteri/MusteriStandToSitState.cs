using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusteriStandToSitState : MusteriState
{
    public override void StartState(Action action)
    {
        action.CustomerSit();
        customer.transform.LookAt(customer.chair.platePlace);
    }
    public override void UpdateState(Action action)
    {
        
    }
    
}

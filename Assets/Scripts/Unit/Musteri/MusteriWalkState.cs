using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusteriWalkState : MusteriState
{
    public override void StartState(Action action)
    {
        item = kapi.instance;
        item.CreateQueue(customer);
        if(item.queue[0] != customer)
        {
            customer.queueState.isCarrying = false;
            customer.queueState.previousState = customer.currState;
            customer.currState = customer.queueState;
            return;
        }

        if(customer.chair == null)
        {
            action.CustomerStandIdle();
            customer.currState = customer.musteriChairBekleState;
            return;
        }
        item.UpdateQueue(customer);        
       
        action.CustomerWalk();
        StartCoroutine(GameManager.instance.SetDestinationCouroutine(customer.chair.placeToSit.position,customer,this));
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(customer.agent.transform.position,customer.placeToSit.transform.position) > .3f)
        {
            return;
        }
        transform.LookAt(customer.chair.platePlace);

        customer.agent.isStopped = true;
        customer.chair.SetMusteri(customer);
        customer.currState = customer.standToSitState;
    }
}

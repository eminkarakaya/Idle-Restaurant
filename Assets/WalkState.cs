using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : GarsonState
{   
    Garson garson;
    public override void StartState(Action action)
    {
        garson = GetComponentInParent<Garson>();
        if(garson.agent == null)
        {
            garson.agent = garson.GetComponent<NavMeshAgent>();
        }
        garson.agent.SetDestination(garson.counter.garsonPos.position);
        Debug.Log("startstate " + this.name);
    }
    public override GarsonState UpdateState(Action action)
    {
        action.Yuru();
        if(GoToCounter())
        {
            return this;
        }
        return garson.tasiState;
    }
    public bool GoToCounter()
    {
        // Debug.Log(Vector3.Distance(garson.agent.transform.position,garson.counter.transform.position) + " remainigdistanx");
        if(Vector3.Distance(garson.agent.transform.position,garson.counter.transform.position) > 0.5f)
        {
            
            return true;
        }
        
        return false;
    }
}

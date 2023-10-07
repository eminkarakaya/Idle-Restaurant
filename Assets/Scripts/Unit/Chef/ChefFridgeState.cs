using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefFridgeState : ChefBaseState
{
    Transform _fridgeTransform;
    GameObject _pastry;
    GameObject pastry;

    public override void StartState(Action action)
    {
        pastry = chef.pastry;
        _fridgeTransform = chef.fridge;
        chef.agent.SetDestination(_fridgeTransform.position);
        action.Walk();
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(chef.agent.transform.position,_fridgeTransform.position) > 0.7f)
        {
            return;
        }
        chef.rollOutPizzaState.pastry = Instantiate(pastry,chef.hand.position,Quaternion.identity,chef.hand);
        chef.currState = chef.rollOutPizzaState;
        
    }
}

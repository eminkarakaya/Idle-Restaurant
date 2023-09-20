using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollOutPizzaState : ChefBaseState
{
    public GameObject hamur;
    public override void StartState(Action action)
    {
        item = chef.rollOutPizzaCounter;
        item.CreateQueue(chef);
        if(item.queue[0] != chef)
        {
            chef.queueState.previousState = chef.currState;
            chef.queueState.isCarrying = true;
            chef.currState = chef.queueState;
            return;
        }
        
        chef.agent.SetDestination(item.chefPlace.position);
        action.Carry();
        
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(chef.agent.transform.position,item.chefPlace.position) > 0.5f)
            return;
        Destroy(hamur);
        var pizza =  ObjectPool.instance.GetPooledObject(0);
        pizza.transform.position = item.platePlaces[0].position;
        pizza.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
        // chef.pizza = Instantiate(chef.pizzaPrefab,item.platePlaces[item.platePlaces.Count-1].position,Quaternion.Euler(new Vector3(-90,0,0)));
        chef.pizza = pizza;
        chef.pizza.transform.SetParent(null);
        chef.asciIdleState.rollOutPizzaCounter = item.GetComponent<RollOutPizzaCounter>();
        chef.currState = chef.asciIdleState;
    }
}

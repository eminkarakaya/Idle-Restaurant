using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountereKoymaState : AsciState
{
    Asci asci;
    Counter counter;
    public GameObject pizza;
    public override void StartState(Action action)
    {
        asci = GetComponentInParent<Asci>();
        counter = asci.counter;
        asci.agent.SetDestination(counter.asciPos.position);
        action.AsciTasi();
    }
    public override void UpdateState(Action action)
    {
        
        if(Vector3.Distance(asci.agent.transform.position,counter.asciPos.position) > .4f)
        {
            return;
        }
        if(counter.isFull)
        {
            asci.counterFullState.pizza = pizza;
            asci.counterFullState.counter = this.counter;
            asci.currState = asci.counterFullState;
        }
        pizza.transform.SetParent(null);
        pizza.transform.position = counter.platePos.position;
        counter.food = pizza;
        counter.isFull = true;
        asci.level.yemegiHazirCounterler.Add(counter);  
        pizza.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
        asci.currState = asci.buzdolabiState;
    }
}

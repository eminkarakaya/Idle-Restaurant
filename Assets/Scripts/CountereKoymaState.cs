using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountereKoymaState : AsciState
{
    [SerializeField] Asci asci;
    [SerializeField] Counter counter;
    public GameObject pizza;
    public override void StartState(Action action)
    {
        asci = GetComponentInParent<Asci>();
        item = asci.counter;
        counter = asci.counter;
        item.CreateQueue(asci);
        if(item.queue.Count > 1)
        {
            asci.queueState.oncekiState = asci.currState;
            asci.queueState.oncekiAction = asci.action;
            asci.currState = asci.queueState;
        }
        // pizza = asci.pizza;
        action.AsciTasi();
    }
    public override void UpdateState(Action action)
    {
        
        asci.agent.SetDestination(item.asciYeri.position);
        Debug.Log(item + " weodjfad");
        if(Vector3.Distance(asci.agent.transform.position,counter.asciYeri.position) > .4f)
        {
            return;
        }
        // if(counter.isFull)
        // {
        //     asci.counterFullState.pizza = pizza;
        //     asci.counterFullState.counter = this.counter;
        //     asci.currState = asci.counterFullState;
        // }
        item.UpdateQueue(asci);
        pizza.transform.SetParent(null);
        pizza.transform.position = counter.tabakYeri.position;
        counter.food = pizza;
        counter.isFull = true;
        asci.level.yemegiHazirCounterler.Add(counter);  
        pizza.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
        asci.currState = asci.buzdolabiState;
    }
}

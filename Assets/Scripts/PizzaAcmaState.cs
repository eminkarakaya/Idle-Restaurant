using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaAcmaState : AsciState
{
    public GameObject hamur;
    Asci asci;
    GameObject _pizza;
    GameObject pizza;
    public override void StartState(Action action)
    {
        asci = GetComponentInParent<Asci>();
        item = asci.pizzaAcmaCounter;
        item.CreateQueue(asci);
        if(item.queue.Count > 1)
        {
            asci.queueState.oncekiState = asci.currState;
            asci.queueState.oncekiAction = asci.action;
            asci.currState = asci.queueState;
        }
        asci.agent.SetDestination(item.asciYeri.position);
        pizza = asci.pizza;
        action.AsciTasi();
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(asci.agent.transform.position,item.transform.GetChild(0).position) > 0.5f)
            return;
        Destroy(hamur);
        _pizza = Instantiate(pizza,item.tabakYeri.position,Quaternion.Euler(new Vector3(-90,0,0)));
        _pizza.transform.SetParent(null);
        asci.asciIdleState.pizza = _pizza;
        asci.asciIdleState.pizzaAcmaCounter = item.GetComponent<PizzaAcmaCounter>();
        asci.currState = asci.asciIdleState;
    }
}

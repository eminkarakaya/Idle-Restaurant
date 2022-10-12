using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaAcmaState : AsciState
{
    public GameObject hamur;
    public override void StartState(Action action)
    {
        item = asci.pizzaAcmaCounter;
        item.CreateQueue(asci);
        if(item.queue[0] != asci)
        {
            asci.queueState.oncekiState = asci.currState;
            asci.queueState.oncekiAction = asci.action;
            asci.currState = asci.queueState;
            return;
        }
        
        asci.agent.SetDestination(item.asciYeri.position);
        action.Tasi();
        
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(asci.agent.transform.position,item.transform.GetChild(0).position) > 0.5f)
            return;
        Destroy(hamur);
        asci.pizza = Instantiate(asci.pizzaPrefab,item.tabakYerleri[item.tabakYerleri.Count-1].position,Quaternion.Euler(new Vector3(-90,0,0)));
        asci.pizza.transform.SetParent(null);
        asci.asciIdleState.pizzaAcmaCounter = item.GetComponent<PizzaAcmaCounter>();
        asci.currState = asci.asciIdleState;
    }
}

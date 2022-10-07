using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirinaKoymaState : AsciState
{
    public GameObject pizza;
    Asci asci;
    Transform firinTransform;
    float offset = .5f;
    void Start()
    {
        
    }
    public override void StartState(Action action)
    {
        asci = GetComponentInParent<Asci>();
        item = asci.ocak;
        firinTransform = item.asciYeri.transform;
        if(!item.queue.Contains(asci))
            item.CreateQueue(asci);
        if(item.queue.Count > 1)
        {
            asci.queueState.oncekiState = asci.currState;
            asci.queueState.oncekiAction = asci.action;
            asci.currState = asci.queueState;
        }
        asci.agent.SetDestination(firinTransform.position);
        action.AsciTasi();
        
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(asci.agent.transform.position,firinTransform.position) > offset)
        {
            return;
        }
        pizza.transform.SetParent(null);
        pizza.transform.position = item.tabakYeri.position;
        pizza.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
        asci.firiniBeklemeState.pizza = pizza;
        pizza = null;
        
        asci.currState = asci.firiniBeklemeState;
        
    }
}

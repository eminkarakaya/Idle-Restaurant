using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirinaKoymaState : AsciState
{
    Transform firinTransform;
    float offset = .5f;
    public override void StartState(Action action)
    {
        item = asci.ocak;
        firinTransform = item.asciYeri.transform;
        if(!item.queue.Contains(asci))
            item.CreateQueue(asci);
        
        if(item.queue[0] != asci)//&& item.queue[0] != asci)
        {
            asci.queueState.oncekiState = asci.currState;
            asci.queueState.oncekiAction = asci.action;
            asci.currState = asci.queueState;
        }
        asci.agent.SetDestination(firinTransform.position);
        action.Tasi();
        
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(asci.agent.transform.position,firinTransform.position) > offset)
        {
            return;
        }
        asci.pizza.transform.SetParent(null);
        asci.pizza.transform.position = item.tabakYerleri[item.tabakSayisi-1].position;
        asci.pizza.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
        // asci.pizza = null;
        
        asci.currState = asci.firiniBeklemeState;
        
    }
}

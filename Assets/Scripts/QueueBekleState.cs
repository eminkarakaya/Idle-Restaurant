using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueBekleState : AsciState
{
    public AsciState oncekiState;
    Asci asci;
    public override void StartState(Action action)
    {
        asci = GetComponentInParent<Asci>();
        asci.transform.LookAt(item.transform);
        asci.agent.isStopped = true;
    }
    public override void UpdateState(Action action)
    {
        if(item.queue[0] == asci)
        {
            asci.agent.isStopped = false;
            asci.currState = oncekiState;
        }
    }
}

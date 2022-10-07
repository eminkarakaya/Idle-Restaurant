using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueState : AsciState
{
    public Action oncekiAction;
    public AsciState oncekiState;
    Asci asci;
    private Vector3 _queuePlace;
    public Vector3 queuePlace{
        get => _queuePlace;
        set{

            _queuePlace = value;
        }
    }
    public override void StartState(Action action)
    {
        asci = GetComponentInParent<Asci>();
        asci.agent.SetDestination(_queuePlace);
        
        item = oncekiState.item;
        Debug.Log(item + " item ");
        Debug.Log(item.queue[0] + " quqeue0 ");
        if(item.queue[0] == item)
        {
            asci.currState = oncekiState;
        }
    }
    public override void UpdateState(Action action)
    {
       
        if(Vector3.Distance(asci.transform.position,_queuePlace) < .2f)
        {
            action.AsciYemekleDur();
            asci.currState = asci.queueBekleState;
        }
        else
            action.AsciTasi();
        
    }
}

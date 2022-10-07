using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueState : AsciState
{
    public Action oncekiAction;
    public AsciState oncekiState;
    Asci asci;
    [SerializeField] private Vector3 _queuePlace;
    public Vector3 queuePlace{
        get => _queuePlace;
        set{

            _queuePlace = value;
        }
    }
    public override void StartState(Action action)
    {
        asci = GetComponentInParent<Asci>();
        
        item = oncekiState.item;
        if(item.queue[0] == asci)
        {
            Debug.Log(asci.currState + " " + asci);
            // asci.currState = oncekiState;
            // return;
        }
        _queuePlace = item.createdQueueTransform[item.createdQueueTransform.Count-1].position;
        asci.agent.SetDestination(_queuePlace);
    }
    public override void UpdateState(Action action)
    {
        if(item.queue[0] == asci)
        {
            Debug.Log(asci.currState + " " + asci);
            asci.currState = oncekiState;
            // return;
        }
        if(Vector3.Distance(asci.transform.position,_queuePlace) < .4f)
        {
            Debug.Log("beklemey");
            action.AsciYemekleDur();
            asci.queueBekleState.item = item;
            asci.queueBekleState.oncekiState = oncekiState;
            asci.currState = asci.queueBekleState;
        }
        else
        {

            action.AsciTasi();
        }
        
    }
}

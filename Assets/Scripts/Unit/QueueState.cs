using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueState : StateBase
{
    public bool isCarrying;

    public bool isUpdate;
    public StateBase previousState;
    Unit unit;
    [SerializeField] private Vector3 _queuePlace;
    public Vector3 queuePlace{
        get => _queuePlace;
        set{

            _queuePlace = value;
        }
    }
    public override void StartState(Action action)
    {
        unit = GetComponentInParent<Unit>();
        unit.queueImage.gameObject.SetActive(true);
        item = previousState.item;
        if(!isUpdate)
        {   
            _queuePlace = item.createdQueueTransform[item.queue.Count-1].position;
        }
        isUpdate = false;
        if(isCarrying)
        {
            action.Carry();
        }
        else
        {
            action.Walk();
        }
        
    }
    public override void UpdateState(Action action)
    {   
        unit.agent.SetDestination(_queuePlace);
        if(Vector3.Distance(unit.transform.position,_queuePlace) < .7f)
        {
            unit.queueWaitState.item = item;
            unit.queueWaitState.previousState = previousState;
            unit.queueWaitState.isCarrying = isCarrying;
            unit.currState = unit.queueWaitState;
        }
    }
}

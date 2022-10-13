using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueState : StateBase
{
    public bool isUpdate;
    public Action oncekiAction;
    public StateBase oncekiState;
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
        unit.bekleImage.gameObject.SetActive(true);
        // asci = GetComponentInParent<Asci>();
        item = oncekiState.item;
        Debug.Log(oncekiState);
        Debug.Log(item + " item");
        if(!isUpdate)
        {
            Debug.Log(item);
            Debug.Log(item.createdQueueTransform);
            Debug.Log(item.queue.Count);
            Debug.Log(item.createdQueueTransform[item.queue.Count]);
            _queuePlace = item.createdQueueTransform[item.queue.Count].position;

        }
        // Debug.Log(item.createdQueueTransform[item.createdQueueTransform.Count-1].position.normalized + " qwfdvsjkbfasj",item.createdQueueTransform[item.createdQueueTransform.Count-1]);
        isUpdate = false;
        
    }
    public override void UpdateState(Action action)
    {
        if(unit.TryGetComponent(out Musteri musteri))
        {
            var chair = musteri.FindEmptyChair();
            if(item.queue[0] == unit && chair != null)
            {
                musteri.chair = chair;
                unit.bekleImage.gameObject.SetActive(false);
                musteri.currState = oncekiState;
            }
        }
        else if(item.queue[0] == unit)// && unit.isReady)
        {
            unit.bekleImage.gameObject.SetActive(false);
            Debug.Log(unit + " queuemunit");
            unit.currState = oncekiState;
        }
        unit.agent.SetDestination(_queuePlace);
        if(Vector3.Distance(unit.transform.position,_queuePlace) < .7f)
        {
            action.YemekleDur();
            unit.queueBekleState.item = item;
            unit.queueBekleState.oncekiState = oncekiState;
            unit.currState = unit.queueBekleState;
        }
        // else
        // {
        //     action.AsciTasi();
        // }
    }
}

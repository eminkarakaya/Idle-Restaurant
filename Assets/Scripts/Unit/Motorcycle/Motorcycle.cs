using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Motorcycle : Unit
{
    public GameObject pizza;
    public Item sira;
    public Transform baslangicPos;
    public WaitOffMapState mapDisindaBekleState;
    public TakeOrderState siparisiAlState;
    public WaitOrderState siparisiBekleState;
    public DeliverOrderState siparisiGoturState;
    public Counter counter;
    public Transform garsonPos;
    public Takeaway parkinLot;
    public Transform tabakPos;
    
    void Awake()
    {
        action = GetComponent<Action>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        currState = siparisiAlState;
    }
    void Update()
    {
        currState.UpdateState(action);
    }
    public Counter FindCounter()
    {
        if(parkinLot.foodReadyCounters.Count == 0)
            return null;
        Counter nearestCounter = parkinLot.foodReadyCounters[0];
        float nearestDistance = Vector3.Distance(parkinLot.foodReadyCounters[0].transform.position,this.transform.position);
        for (int i = 0; i < parkinLot.foodReadyCounters.Count; i++)
        {
            float distance = Vector3.Distance(parkinLot.foodReadyCounters[i].transform.position,transform.position);
            if(distance < nearestDistance)
            {
                nearestCounter = parkinLot.foodReadyCounters[i];
            }
        }
        var _counter = nearestCounter;
        garsonPos = _counter.waiterPos;
        parkinLot.foodReadyCounters.Remove(nearestCounter);
        this.counter = _counter;
        return _counter;
    }
    
}

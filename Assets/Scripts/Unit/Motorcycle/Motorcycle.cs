using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Motorcycle : Unit
{
    public GameObject pizza;
    public Item sira;
    public Transform baslangicPos;
    public MapDisindaBekleState mapDisindaBekleState;
    public SiparisiAlState siparisiAlState;
    public SiparisiBekleState siparisiBekleState;
    public SiparisiGoturState siparisiGoturState;
    public Counter counter;
    public Transform garsonPos;
    public ParkinLot parkinLot;
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
        if(parkinLot.yemegiHazirCounterler.Count == 0)
            return null;
        Counter nearestCounter = parkinLot.yemegiHazirCounterler[0];
        float nearestDistance = Vector3.Distance(parkinLot.yemegiHazirCounterler[0].transform.position,this.transform.position);
        for (int i = 0; i < parkinLot.yemegiHazirCounterler.Count; i++)
        {
            float distance = Vector3.Distance(parkinLot.yemegiHazirCounterler[i].transform.position,transform.position);
            if(distance < nearestDistance)
            {
                nearestCounter = parkinLot.yemegiHazirCounterler[i];
            }
        }
        var _counter = nearestCounter;
        garsonPos = _counter.garsonPos;
        parkinLot.yemegiHazirCounterler.Remove(nearestCounter);
        this.counter = _counter;
        return _counter;
    }
    
}

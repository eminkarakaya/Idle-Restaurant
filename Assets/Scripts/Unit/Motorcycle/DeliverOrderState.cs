using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverOrderState : MotorcycleState
{
    public Transform mapDisi;
    public float time;
    float timeTemp;
    public override void StartState(Action action)
    {
        mapDisi = motorcycle.parkinLot.finishPos;
        motorcycle.agent.SetDestination(mapDisi.position);
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(motorcycle.transform.position,mapDisi.position)<3f)
        {
            motorcycle.currState = motorcycle.mapDisindaBekleState;
        }
    }
}

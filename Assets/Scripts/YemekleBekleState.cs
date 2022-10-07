using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YemekleBekleState : GarsonState
{
    Garson garson;
    void Start()
    {
        garson = GetComponentInParent<Garson>();
    }
    public override void StartState(Action action)
    {
        action.YemekleDur();
    }
    public override void UpdateState(Action action)
    {
        var chair = garson.FindChair();
        if(chair != null)
        {
            garson.currentState =  garson.tasiState;
        }
        return;
    }
}

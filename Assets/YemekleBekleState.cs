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
        Debug.Log("startstate " + this.name);
    }
    public override GarsonState UpdateState(Action action)
    {
        if(garson._chair != null)
        {
            return garson.tasiState;
        }
        return this;
    }
}

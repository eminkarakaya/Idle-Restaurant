using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasiState : GarsonState
{
    Garson garson;
    public override void StartState(Action action)
    {
        garson = GetComponentInParent<Garson>();
        if(!garson.eliDolumu)
        {
            action.Tasi();
            garson._plate = Instantiate(garson.plate,garson.hand.position,Quaternion.Euler(new Vector3(-90,0,0)),garson.hand.transform);
            garson.counter.food.transform.SetParent(garson._plate.transform.GetChild(0).transform);
            garson.counter.food.transform.localPosition = new Vector3(0,0.06f,0);// Vector3.zero;
            garson.counter.food = null;
            garson.counter.isFull = false;
            garson.eliDolumu = true;
        }
    }
    public override GarsonState UpdateState(Action action)
    {
        var chair = garson.FindChair();
        if(garson._chair == null)
        {
            return garson.yemekleBekleState;
        }
        garson.agent.SetDestination(garson._chair.tabakYeri.position);
        Debug.Log(Vector3.Distance(garson.agent.transform.position,garson._chair.tabakYeri.transform.position) + " qwe");
        if(Vector3.Distance(garson.agent.transform.position,garson._chair.tabakYeri.transform.position) > 1f)
        {
            return this;
        }        
        else
        {
            garson._plate.transform.position = garson._chair.tabakYeri.transform.position;
            garson._plate.transform.SetParent(null);
            garson._chair.GetMusteri().YemeginGelmesi();
            garson._chair.pizza = garson._pizza;
            garson._chair.tabak = garson._plate;
            garson.eliDolumu = false;
            garson._chair = null;
            return garson.walkState;
        }
    }
}

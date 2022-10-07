using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasiState : GarsonState
{
    Garson garson;
    public override void StartState(Action action)
    {
        garson = GetComponentInParent<Garson>();
        // if(garson.counter.food == null)
        // {
        //     garson.currentState = garson.idleState;
        //     return;
        // }
        action.Tasi();
        if(!garson.eliDolumu)
        {
            if(garson.counter.food == null)
            {
                garson.currentState = garson.idleState;
                return;
            }
            action.Tasi();
            garson._plate = Instantiate(garson.plate,garson.hand.position,Quaternion.Euler(new Vector3(-90,0,0)),garson.hand.transform);
            garson.counter.food.transform.SetParent(garson._plate.transform.GetChild(0).transform);
            garson.counter.food.transform.localPosition = new Vector3(0,0.06f,0);// Vector3.zero;
            garson.counter.food = null;
            garson.counter.isFull = false;
            garson.eliDolumu = true;
            garson._chair = garson.FindChair();
        }
        else
        {
            garson.agent.SetDestination(garson.beklemeYeri.position);
        }
    }
    public override void UpdateState(Action action)
    {
        
        if(garson._chair == null)
        {
            if(Vector3.Distance(garson.agent.transform.position, garson.beklemeYeri.position) < 0.3f)
            {
                garson.currentState = garson.yemekleBekleState; 
                return;
            }
            garson.currentState = garson.tasiState;
            // garson.currentState = garson.yemekleBekleState;
            return;
        }
        garson.agent.SetDestination(garson._chair.tabakYeri.position);
        if(Vector3.Distance(garson.agent.transform.position,garson._chair.tabakYeri.transform.position) > 1f)
        {
            return;
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
            garson.counter = null;
            garson.currentState = garson.walkState;
        }
    }
}

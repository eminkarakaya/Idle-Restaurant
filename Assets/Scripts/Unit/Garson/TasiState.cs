using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasiState : GarsonState
{
    public override void StartState(Action action)
    {
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
                garson.currState = garson.idleState;
                return;
            }
            action.Tasi();
            garson._plate = Instantiate(garson.plate,garson.hand[garson.handSayisi-1].position,Quaternion.Euler(new Vector3(-90,0,0)),garson.hand[garson.handSayisi-1].transform);
            garson.counter.food.transform.SetParent(garson._plate.transform.GetChild(0).transform);
            garson.counter.food.transform.localPosition = new Vector3(0,0.06f,0);// Vector3.zero;
            garson.counter.food = null;
            garson.counter.isFull = false;
            garson.eliDolumu = true;
        }
        else
        {
            garson.agent.SetDestination(garson.beklemeYeri.position);
        }
    }
    public override void UpdateState(Action action)
    {
        // if(garson._chair == null)
        // {
        //     if(Vector3.Distance(garson.agent.transform.position, garson.beklemeYeri.position) < 0.3f)
        //     {
        //         garson.currState = garson.yemekleBekleState; 
        //         return;
        //     }
        //     garson.currState = garson.tasiState;
        //     // garson.currState = garson.yemekleBekleState;
        //     return;
        // }
        garson.agent.SetDestination(garson._chair.tabakYeri.position);
        if(Vector3.Distance(garson.agent.transform.position,garson._chair.tabakYeri.transform.position) > 1f)
        {
            return;
        }        
        else
        {
            garson._plate.transform.position = garson._chair.tabakYeri.transform.position;
            garson._plate.transform.SetParent(null);
            garson._chair.pizza = garson._pizza;
            garson._chair.tabak = garson._plate;
            garson.eliDolumu = false;
            var musteri = garson._chair.GetMusteri();
            musteri.currState = musteri.musteriEatingState;
            garson._chair = null;
            garson.counter = null;
            garson.currState = garson.beklemeYerineGitState;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasiState : GarsonState
{
    public override void StartState(Action action)
    {
        action.Carry();
        if(!waiter.isHandFull)
        {
            if(waiter.counter.food == null)
            {
                waiter.currState = waiter.idleState;
                return;
            }
            action.Carry();
            var plate = ObjectPool.instance.GetPooledObject(1);
            plate.transform.position = waiter.hand.position;
            plate.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
            plate.transform.SetParent(waiter.hand.transform);

            // waiter._plate = Instantiate(waiter.plate,waiter.hand.position,Quaternion.Euler(new Vector3(-90,0,0)),waiter.hand.transform);
            waiter._plate = plate.GetComponent<Tabak>();
            waiter.counter.food.transform.SetParent(waiter._plate.transform.GetChild(0).transform);
            waiter.counter.food.transform.localPosition = new Vector3(0,0.06f,0);// Vector3.zero;
            waiter.counter.food = null;
            waiter.counter.isFull = false;
            waiter.isHandFull = true;
        }
        else
        {
            waiter.agent.SetDestination(waiter.waitingPlace.position);
        }
    }
    public override void UpdateState(Action action)
    {
        waiter.agent.SetDestination(waiter._chair.platePlace.position);
        if(Vector3.Distance(waiter.agent.transform.position,waiter._chair.platePlace.transform.position) < 1f)
        {
            waiter._plate.transform.position = waiter._chair.platePlace.transform.position;
            waiter._plate.transform.SetParent(null);
            waiter._chair.pizza = waiter._pizza;
            waiter._chair.plate = waiter._plate;
            waiter.isHandFull = false;
            var musteri = waiter._chair.GetMusteri();
            musteri.currState = musteri.customerEatingState;
            waiter._chair = null;
            waiter.counter = null;
            waiter.currState = waiter.beklemeYerineGitState;
        }
    }
}

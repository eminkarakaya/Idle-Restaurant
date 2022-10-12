using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsciIdleState : AsciState
{
    public PizzaAcmaCounter pizzaAcmaCounter;
    public float pizzaAcmaTime = 0;
    float _pizzaAcmaTimeTemp;
    public override void StartState(Action action)
    {
        _pizzaAcmaTimeTemp = pizzaAcmaTime;
        action.Idle();
    }
    public override void UpdateState(Action action)
    {
        _pizzaAcmaTimeTemp -= Time.deltaTime;
        if(_pizzaAcmaTimeTemp < 0)
        {
            pizzaAcmaCounter.UpdateQueue(asci);
            _pizzaAcmaTimeTemp = pizzaAcmaTime;
            asci.pizza.transform.position = asci.hand[asci.handSayisi-1].position;
            asci.pizza.transform.SetParent(asci.hand[asci.handSayisi-1]);
            // asci.pizza = null;
            asci.currState = asci.firinaKoymaState;
        }
    }
}

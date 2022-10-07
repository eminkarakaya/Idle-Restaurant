using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsciIdleState : AsciState
{
    public PizzaAcmaCounter pizzaAcmaCounter;
    public float pizzaAcmaTime = 0;
    float _pizzaAcmaTimeTemp;
    Asci asci;
    [HideInInspector] public GameObject pizza;
    public override void StartState(Action action)
    {
        _pizzaAcmaTimeTemp = pizzaAcmaTime;
        asci = GetComponentInParent<Asci>();
        action.AsciIdle();
    }
    public override void UpdateState(Action action)
    {
        _pizzaAcmaTimeTemp -= Time.deltaTime;
        if(_pizzaAcmaTimeTemp < 0)
        {
            pizzaAcmaCounter.UpdateQueue(asci);
            _pizzaAcmaTimeTemp = pizzaAcmaTime;
            pizza.transform.position = asci.hand.position;
            pizza.transform.SetParent(asci.hand);
            asci.firinaKoymaState.pizza = this.pizza;
            pizza = null;
            asci.currState = asci.firinaKoymaState;
        }
    }
}

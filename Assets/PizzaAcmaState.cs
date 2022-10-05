using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaAcmaState : AsciState
{
    Asci asci;
    PizzaAcmaCounter _pizzaAcmaCounter;
    GameObject _pizza;
    GameObject pizza;
    public override void StartState(Action action)
    {
        asci = GetComponentInParent<Asci>();
        asci.agent.SetDestination(_pizzaAcmaCounter.place.position);
        
    }
    public override AsciState UpdateState(Action action)
    {
        if(Vector3.Distance( asci.agent.transform.position,_pizzaAcmaCounter.place.position) > 0.5f)
            return this;
        _pizza = Instantiate(pizza,_pizzaAcmaCounter.place.position,Quaternion.Euler(new Vector3(-90,0,0)));
        _pizza.transform.SetParent(null);
        //bekleme sataeis olusturcak
        //
        _pizza.transform.position = asci.hand.position;
        _pizza.transform.SetParent(asci.hand);
        return asci.firinaKoymaState;
    }
}

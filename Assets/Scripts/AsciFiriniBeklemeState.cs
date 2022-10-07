using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsciFiriniBeklemeState : AsciState
{
    public GameObject pizza;
    public Asci asci;
    public Ocak ocak;
    float time = 5f;
    float _timeTemp;
    public override void StartState(Action action)
    {
       asci = GetComponentInParent<Asci>();
       ocak = asci.ocak;
       action.AsciIdle();
       ocak.ocakLight.enabled = true;
    }
    public override void UpdateState(Action action)
    {
        _timeTemp -= Time.deltaTime;
        if(_timeTemp <= 0)
        {
            ocak.UpdateQueue(asci);
            pizza.transform.SetParent(asci.hand);
            pizza.transform.position = asci.hand.transform.position;
            _timeTemp = time;
            asci.countereKoymaState.pizza = pizza;
            pizza = null;
            ocak.ocakLight.enabled = false;
            asci.currState = asci.countereKoymaState;
        }
    }
}

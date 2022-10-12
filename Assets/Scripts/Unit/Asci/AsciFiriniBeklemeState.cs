using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsciFiriniBeklemeState : AsciState
{
    public Ocak ocak;
    float time = 1f;
    float _timeTemp;
    public override void StartState(Action action)
    {
        asci.slider.gameObject.SetActive(true);
        asci.slider.maxValue = time;
        asci.slider.value = 0;
        _timeTemp = 0;
       ocak = asci.ocak;
       action.Idle();
       ocak.ocakLight.enabled = true;
    }
    public override void UpdateState(Action action)
    {
        _timeTemp += Time.deltaTime;
        asci.slider.value = _timeTemp;
        if(_timeTemp >= time)
        {
            ocak.UpdateQueue(asci);
            asci.pizza.transform.SetParent(asci.hand[asci.handSayisi-1]);
            asci.pizza.transform.position = asci.hand[asci.handSayisi-1].transform.position;
            _timeTemp = time;
            // asci.pizza = null;
            ocak.ocakLight.enabled = false;
            asci.slider.gameObject.SetActive(false);
            asci.currState = asci.countereKoymaState;
        }
    }
}

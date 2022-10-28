using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsciFiriniBeklemeState : AsciState
{
    public Ocak oven;
    public float time = 1f;
    float _timeTemp;
    public override void StartState(Action action)
    {
        chef.slider.gameObject.SetActive(true);
        chef.slider.maxValue = time;
        chef.slider.value = 0;
        _timeTemp = 0;
       oven = chef.oven;
       action.Idle();
       oven.ovenLight.enabled = true;
    }
    public override void UpdateState(Action action)
    {
        _timeTemp += Time.deltaTime;
        chef.slider.value = _timeTemp;
        if(_timeTemp >= time)
        {
            oven.UpdateQueue(chef);
            chef.pizza.transform.SetParent(chef.hand);
            chef.pizza.transform.position = chef.hand.transform.position;
            _timeTemp = time;
            // asci.pizza = null;
            oven.ovenLight.enabled = false;
            chef.slider.gameObject.SetActive(false);
            chef.currState = chef.putOnCounterState;
        }
    }
}

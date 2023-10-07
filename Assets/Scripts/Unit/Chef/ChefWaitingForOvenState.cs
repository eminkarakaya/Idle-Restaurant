using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChefWaitingForOvenState : ChefBaseState
{
    AudioSource audioSource;
    [SerializeField] private AudioClip ovenDingClip;
    public Oven oven;
    public float time = 1f;
    float _timeTemp;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
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
            oven.VoiceCanvas();
            audioSource.PlayOneShot(ovenDingClip);
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

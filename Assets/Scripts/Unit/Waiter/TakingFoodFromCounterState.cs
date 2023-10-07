using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingFoodFromCounterState : WaiterBaseState
{
    AudioSource audioSource;
    [SerializeField] private AudioClip puttingTableAudio;
    public Counter counter;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public override void StartState(Action action)
    {
        action.Walk();
    }
    public override void UpdateState(Action action)
    {
        waiter.agent.SetDestination(counter.waiterPos.position);
        if(Vector3.Distance(waiter.agent.transform.position,counter.transform.position) < 0.8f)
        {
            waiter.tasiState.waiter.counter = counter;
            audioSource.PlayOneShot(puttingTableAudio);
            counter = null;
            waiter.currState = waiter.tasiState;   
        }  
    }

}

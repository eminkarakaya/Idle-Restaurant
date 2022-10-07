using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterFullState : AsciState
{
    Asci asci;
    public GameObject pizza;
    [HideInInspector] public Counter counter;
    public override void StartState(Action action)
    {
        asci = GetComponentInParent<Asci>();
        action.AsciIdle();
    }
    public override void UpdateState(Action action)
    {
        if(!counter.isFull)
        {
            asci.countereKoymaState.pizza = pizza;
            asci.currState = asci.countereKoymaState;
        }
    }
}

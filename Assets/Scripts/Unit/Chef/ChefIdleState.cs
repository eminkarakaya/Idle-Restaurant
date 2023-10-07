using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefIdleState : ChefBaseState
{
    // pızza acma state
    public RollOutPizzaCounter rollOutPizzaCounter;
    public float rollOutPizzaTime = 0;
    float _rollOutTimeTemp;
    public override void StartState(Action action)
    {
        _rollOutTimeTemp = rollOutPizzaTime;
        action.Idle();
    }
    public override void UpdateState(Action action)
    {
        _rollOutTimeTemp -= Time.deltaTime;
        if(_rollOutTimeTemp < 0)
        {
            rollOutPizzaCounter.UpdateQueue(chef);
            _rollOutTimeTemp = rollOutPizzaTime;
            chef.pizza.transform.position = chef.hand.position;
            chef.pizza.transform.SetParent(chef.hand);
            // asci.pizza = null;
        // pizza actıktan sonra fırına gıdıyor
            chef.currState = chef.putOunOvenState;
        }
    }
}

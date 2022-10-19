using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkBekleState : BulasikciState
{
    public override void StartState(Action action)
    {
        action.Idle();
        bulasikci.bekleImage.gameObject.SetActive(true);
    }
    public override void UpdateState(Action action)
    {
        if(!bulasikci.sink.isFull)
        {
            bulasikci.bekleImage.gameObject.SetActive(false);
            bulasikci.bulasikCounter.UpdateQueue(bulasikci);
            bulasikci.currState = bulasikci.bulasikciTabakAl;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulasikTabakBekle : BulasikciState
{
    public override void StartState(Action action)
    {
        action.Idle();
        bulasikci.bekleImage.gameObject.SetActive(true);
    }
    public override void UpdateState(Action action)
    {
        if(bulasikci.bulasikCounter.tabaklar.Count != 0)
        {
            bulasikci.bekleImage.gameObject.SetActive(false);
            bulasikci.agent.isStopped = false;
            bulasikci.currState = bulasikci.bulasikciTabakAl;
        }
        else 
        {
            bulasikci.agent.isStopped = true;
        }
    }
}

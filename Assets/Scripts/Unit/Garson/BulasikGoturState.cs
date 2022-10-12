using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulasikGoturState : GarsonState
{
    public Tabak tabak;
    public BulasikCounter bulasikCounter;
    public override void StartState(Action action)
    {
        action.Tasi();
        bulasikCounter = garson.level.restaurant.bulasikCounter;
        garson.agent.SetDestination(bulasikCounter.bulasikciYeri.position);
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(garson.transform.position,bulasikCounter.bulasikciYeri.position) < .4f)
        {
            garson.bulasikGoturIdle.tabak = tabak;
            garson.bulasikGoturIdle.bulasikCounter = bulasikCounter;
            garson.currState = garson.bulasikGoturIdle;   
        }
    }
}

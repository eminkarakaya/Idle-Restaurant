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
        for (int i = 0; i < garson.level.bulasikhane.Count; i++)
        {
            bulasikCounter = garson.level.bulasikhane[i].FindBulasikCounter();
        }
        item = bulasikCounter.garsonItem;
        item.CreateQueue(garson);

        if(item.queue[0] != garson)
        {
            garson.queueState.oncekiState = garson.currState;
            garson.currState = garson.queueState;
        }
        garson.agent.SetDestination(bulasikCounter.bulasikciYeri.position);
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(garson.transform.position,bulasikCounter.bulasikciYeri.position) < .5f || bulasikCounter.tabaklar.Count > 0)
        {
            garson.bulasikGoturIdle.tabak = tabak;
            garson.bulasikGoturIdle.bulasikCounter = bulasikCounter;
            garson.currState = garson.bulasikGoturIdle;
        }
    }
}

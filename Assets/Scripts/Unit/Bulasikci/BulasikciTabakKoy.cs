using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulasikciTabakKoy : BulasikciState
{
    public Sink FindSink()
    {
        List<Sink> allSinks = bulasikci.bulasikhane.kullanilanSinks;
        var enaz = allSinks[0];
        for (int i = 0; i < allSinks.Count; i++)
        {
            if(allSinks[i].bulasikcilar.Count < enaz.bulasikcilar.Count)
            {
                enaz = allSinks[i];
            }
        }
        bulasikci.sink = enaz;
        return enaz;
    }

    public override void StartState(Action action)
    {
        action.Tasi();
        var sink = FindSink();
        item = sink;
        item.CreateQueue(bulasikci);
        if(item.queue[0] != bulasikci)
        {
            bulasikci.queueState.oncekiState = bulasikci.currState;
            bulasikci.currState = bulasikci.queueState;
        }

        bulasikci.agent.SetDestination(sink.asciYeri.position);
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(bulasikci.transform.position,bulasikci.sink.bulasikciYeri.transform.position) < .6f)
        {
            bulasikci.currState = bulasikci.bulasikciYikaState;
        }
    }
}

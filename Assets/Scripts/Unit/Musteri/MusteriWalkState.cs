using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusteriWalkState : MusteriState
{
    public override void StartState(Action action)
    {
        item = kapi.instance;
        if(!item.queue.Contains(musteri))
            item.CreateQueue(musteri);

        if(musteri.chair == null)
            musteri.chair = musteri.FindEmptyChair();
        if(item.queue[0] != musteri || musteri.chair == null)
        {
            musteri.queueState.oncekiAction = musteri.action;
            musteri.queueState.oncekiState = musteri.currState;
            musteri.currState = musteri.queueState;
            return;
        }
        item.UpdateQueue(musteri);
        
       
        action.MusteriYuru();
    }
    public override void UpdateState(Action action)
    {
        musteri.agent.SetDestination(musteri.chair.oturulcakYer.position);
        if(Vector3.Distance(musteri.agent.transform.position,musteri.oturulcakYer.transform.position) > .3f)
        {
            return;
        }
        transform.LookAt(musteri.chair.tabakYeri);

        musteri.agent.isStopped = true;
        musteri.chair.SetMusteri(musteri);
        musteri.currState = musteri.musteriStandToSitState;
    }
}

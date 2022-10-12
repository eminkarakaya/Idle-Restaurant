using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusteriSitToStand : MusteriState
{
    public override void StartState(Action action)
    {
        GoldAnim.instance.EarnGoldAnim2(10000,musteri.transform);
        action.MusteriYuru();
        musteri.chair.MasadanKalkma();
        musteri.agent.isStopped = false;
        musteri.agent.SetDestination(kapi.instance.kapiTransform.position);
    }
    public override void UpdateState(Action action)
    {
        if( Vector3.Distance(musteri.agent.transform.position,kapi.instance.kapiTransform.position) > 0.5f)
        {
            return;
        }
        
        // musteri.level.restaurant.emptyChairs.Add(musteri.chair);
        
        // if(item.queue.Count !=0)
        //     item.queue[0].currState = item.queue[0].GetComponent<Musteri>().musteriWalkState;
        
        Destroy(musteri.gameObject);
    }
}

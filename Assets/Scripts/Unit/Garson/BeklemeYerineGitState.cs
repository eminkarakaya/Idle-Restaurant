using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeklemeYerineGitState : GarsonState
{
    public override void StartState(Action action)
    {
        action.Yuru();
        garson.agent.SetDestination(garson.beklemeYeri.position);
    }
    public override void UpdateState(Action action)
    {
        if(garson.targetKirli == null && garson.level.restaurant.kirliTabaklar.Count !=0)
            garson.targetKirli = garson.level.restaurant.kirliTabaklar[0];
        if(garson.targetKirli != null)
        {
            garson.currState = garson.bulasikToplaState;
            return;
        }
        if(garson._chair == null)
        {
            garson._chair = garson.FindChair();
        }
        if(garson._chair != null)
        {
            garson.currState = garson.yemegiCounterdenAlState;
            return;
        }
        if(Vector3.Distance(garson.transform.position, garson.beklemeYeri.transform.position)<.4f)
        {
            garson.currState = garson.idleState;
        }
    }
}

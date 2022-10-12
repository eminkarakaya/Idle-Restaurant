using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GarsonState
{
    
    public override void StartState(Action action)
    {
        garson.bekleImage.gameObject.SetActive(true);
        garson.action.Idle();
    }
    public override void UpdateState(Action action)
    {
        if(garson.targetKirli == null && garson.level.restaurant.kirliTabaklar.Count !=0)
            garson.targetKirli = garson.level.restaurant.kirliTabaklar[0];
        if(garson.targetKirli != null)
        {
            garson.bekleImage.gameObject.SetActive(false);
            garson.currState = garson.bulasikToplaState;
            return;
        }
        if(garson._chair == null)
        {
            garson._chair = garson.FindChair();
        }
        if(garson._chair != null)
        {
            garson.bekleImage.gameObject.SetActive(false);
            garson.currState = garson.yemegiCounterdenAlState;
            return;
        }
    }    
}

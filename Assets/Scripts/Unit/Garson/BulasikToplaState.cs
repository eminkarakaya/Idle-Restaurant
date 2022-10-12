using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulasikToplaState : GarsonState
{
    // public Chair FindBosTabak()
    // {
    // musteri.level.restaurant.emptyChairs.Add(musteri.chair);
    // }
    public override void StartState(Action action)
    {
        action.Yuru();
        if(garson.level.restaurant.kirliTabaklar.Count != 0)
            garson.targetKirli = garson.level.restaurant.kirliTabaklar[0];
        garson.level.restaurant.kirliTabaklar.Remove(garson.targetKirli);
        garson.level.restaurant.emptyChairs.Add(garson.targetKirli);
    }
    public override void UpdateState(Action action)
    {
        garson.agent.SetDestination(garson.targetKirli.transform.position);
        if(Vector3.Distance(garson.transform.position,garson.targetKirli.transform.position) < .4f)
        {
            garson.targetKirli.tabak.transform.SetParent(garson.transform);
            garson.targetKirli.tabak.transform.position = garson.hand[garson.handSayisi-1].position;

            garson.bulasikGoturState.tabak = garson.targetKirli.tabak;
            garson.currState = garson.bulasikGoturState;
        }
    }
}

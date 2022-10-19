using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulasikGoturIdle : GarsonState
{
    public BulasikCounter bulasikCounter;
    public Tabak tabak;

    public override void StartState(Action action)
    {
        garson.bekleImage.gameObject.SetActive(true);
        item = bulasikCounter.garsonItem;
        action.YemekleDur();
    }
    public override void UpdateState(Action action)
    {
        if(bulasikCounter.tabaklar.Count < 1)
        {
            tabak.transform.SetParent(null);
            tabak.transform.position = bulasikCounter.tabakYerleri[bulasikCounter.tabakSayisi].position;
            garson.queueState.oncekiState = this;
            bulasikCounter.tabaklar.Add(tabak);
            garson.targetKirli = null;
            garson.bulasikGoturState.bulasikCounter.garsonItem.UpdateQueue(garson);
            garson.bekleImage.gameObject.SetActive(false);
            garson.currState = garson.beklemeYerineGitState;
        }
    }
}

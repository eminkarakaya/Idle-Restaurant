using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulasikGoturIdle : GarsonState
{
    public BulasikCounter bulasikCounter;
    public Tabak tabak;
    public override void StartState(Action action)
    {
        action.YemekleDur();
    }
    public override void UpdateState(Action action)
    {
        if(bulasikCounter.tabaklar.Count < bulasikCounter.tabakKapasitesi)
        {
            tabak.transform.SetParent(null);
            tabak.transform.position = bulasikCounter.tabakYerleri[bulasikCounter.tabakSayisi-1].position;
            bulasikCounter.tabaklar.Add(tabak);
            garson.targetKirli = null;
            garson.currState = garson.beklemeYerineGitState;
        }
    }
}

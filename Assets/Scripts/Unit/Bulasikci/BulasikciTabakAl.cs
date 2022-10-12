using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulasikciTabakAl : BulasikciState
{
    public override void StartState(Action action)
    {   
        action.Yuru();
        // var counter = bulasikci.level.bulasikhane.FindBulasikCounter();
        if(bulasikci.counter != null)
        {
            bulasikci.agent.SetDestination(bulasikci.counter.asciYeri.position);
        }
        else
        {
            
        }
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(bulasikci.agent.transform.position,bulasikci.bulasikCounter.bulasikciYeri.position) < .4f)
        {
            if(bulasikci.bulasikCounter.tabaklar.Count == 0)
            {
                Debug.Log("tabaklar bos");
                bulasikci.currState = bulasikci.bulasikTabakBekle;
                return;
            }
            bulasikci.bulasikCounter.tabaklar[bulasikci.bulasikCounter.tabaklar.Count-1].transform.SetParent(bulasikci.transform);
            bulasikci.bulasikCounter.tabaklar[bulasikci.bulasikCounter.tabaklar.Count-1].transform.position = bulasikci.hand[bulasikci.handSayisi-1].position;
            bulasikci.bulasikCounter.tabaklar[bulasikci.bulasikCounter.tabaklar.Count-1].transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
            bulasikci.bulasikciYikaState.tabak = bulasikci.bulasikCounter.tabaklar[bulasikci.bulasikCounter.tabaklar.Count-1];
            bulasikci.bulasikCounter.tabaklar.RemoveAt(bulasikci.bulasikCounter.tabaklar.Count-1);
            bulasikci.currState = bulasikci.bulasikciTabakKoy;
        }
    }
}

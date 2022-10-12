using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulasikciYikaState : BulasikciState
{
    [HideInInspector] public Tabak tabak;
    public float yikamaSuresi;
    float yikamaSuresiTemp;
    public override void StartState(Action action)
    {  
        action.Idle();
        tabak.transform.SetParent(null);
        tabak.transform.position = bulasikci.sink.tabakYerleri[bulasikci.sink.tabakSayisi-1].position;
        bulasikci.slider.gameObject.SetActive(true);
        bulasikci.slider.maxValue = yikamaSuresi;
        bulasikci.slider.value = 0;
    }
    public override void UpdateState(Action action)
    {
        yikamaSuresiTemp += Time.deltaTime;
        bulasikci.slider.value = yikamaSuresiTemp;
        if(yikamaSuresiTemp > yikamaSuresi)
        {
            Destroy(tabak.gameObject);
            yikamaSuresiTemp = 0;
            bulasikci.slider.gameObject.SetActive(false);
            bulasikci.currState = bulasikci.bulasikciTabakAl;
        }
    }
}

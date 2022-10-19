using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulasikhaneData : MonoBehaviour
{
    Level level;
    Bulasikhane bulasikhane;
    [SerializeField] Text sinkSayisi,sinkCost,bulasikciSayisi,
    bulasikciCost,bulasikCounterText,bulasikCounterCost;
    void Start()
    {
        level = GetComponentInParent<Level>();
        bulasikhane = GetComponentInParent<Bulasikhane>();
        UpdateData();
    }
    public void UpdateData()
    {
        sinkSayisi.text =  "Lavabo sayısı : " + bulasikhane.kullanilanSinks.Count + "/" + bulasikhane.allSinks.Count;
        sinkCost.text = GameManager.CaclText(bulasikhane.sinkCost.GetGold());  
        bulasikciSayisi.text = "Bulaşıkçı sayısı : " + bulasikhane.allBulasikci.Count + "/" + bulasikhane.bulasikciKapasitesi;
        bulasikciCost.text = GameManager.CaclText(bulasikhane.bulasikciCost.GetGold());
        bulasikCounterCost.text =   GameManager.CaclText(bulasikhane.bulasikCounterCost.GetGold());
        bulasikCounterText.text = "Tezgah sayısı : " + bulasikhane.kullanilanCounters.Count + "/" + bulasikhane.allBulasikCounter.Count;
    }
}

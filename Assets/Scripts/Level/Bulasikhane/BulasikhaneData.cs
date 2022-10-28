using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulasikhaneData : MonoBehaviour
{
    Level level;
    Bulasikhane scullery;
    [SerializeField] Text sinkCount,sinkCost,dishwasherCount,
    dishwasherCost,dishCounterText,dishCounterCost;
    [SerializeField] TextMeshProUGUI dishWashingTime;
    void Awake()
    {
        level = GetComponentInParent<Level>();
        scullery = GetComponentInParent<Bulasikhane>();
    }
    void Start()
    {
        UpdateData();
        
    }
    public void UpdateData()
    {
        sinkCount.text =  "Lavabo sayısı : " + scullery.currentSinks.Count + "/" + scullery.allSinks.Count;
        sinkCost.text = GameManager.CaclText(scullery.sinkCost.GetGold());  
        dishwasherCount.text = "Bulaşıkçı sayısı : " + scullery.allDishwasher.Count + "/" + scullery.dishwasherCapacity;
        dishwasherCost.text = GameManager.CaclText(scullery.dishwasherCost.GetGold());
        dishCounterCost.text =   GameManager.CaclText(scullery.dishCounterCost.GetGold());
        dishCounterText.text = "Tezgah sayısı : " + scullery.currentCounters.Count + "/" + scullery.allDishCounter.Count;
        if(!scullery.isLocked)
            dishWashingTime.text = GameManager.CaclText(scullery.PizzaMakingTime())+" s";
        else
            dishWashingTime.text = GameManager.CaclText(scullery.unlockCost.GetGold())+"$";

    }
}

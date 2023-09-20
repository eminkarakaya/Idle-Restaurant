using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkinlotData : MonoBehaviour
{
    [SerializeField] ParkinLot parkinLot;
    Level level;
    [SerializeField] Text motorcycleCount
    ,motorAlCostText,motorcycleSpeedText,motorcycleSpeedCost,motorcycleSpeedNext;

    void Start()
    {
        // parkinLot = GetComponentInParent<ParkinLot>();
        level= GetComponentInParent<Level>();
        UpdateData();
    }
    public void UpdateData()
    {
        motorcycleCount.text = parkinLot.allMotorcycle.Count.ToString() + "/" + parkinLot.motorcycleCapacity;
        motorAlCostText.text = GameManager.CaclText(parkinLot.motorCost.GetGold());
        motorcycleSpeedText.text = GameManager.CaclText(parkinLot.hiz) + " m/s";
        motorcycleSpeedCost.text = GameManager.CaclText(parkinLot.motorcycleSpeedCost.GetGold());
        motorcycleSpeedNext.text = "+" + GameManager.CaclText((parkinLot.hiz * (parkinLot.speedIncreasePercentage/100))) + " m/s";
    }
}

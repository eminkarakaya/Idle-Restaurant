using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParkinglotUIData : MonoBehaviour
{
    [SerializeField] ParkinLot parkinLot;
    Level level;
    [SerializeField] private Button buyMotorCycleButton,buySpeedButton;
    [SerializeField] Text motorcycleCount
    ,motorAlCostText,motorcycleSpeedText,motorcycleSpeedCost,motorcycleSpeedNext;

    [SerializeField] TextMeshProUGUI pizzaCikmaSuresi;
    [SerializeField] private GameObject speechBubble;

    void Awake()
    {
        // parkinLot = GetComponentInParent<ParkinLot>();
        level= GetComponentInParent<Level>();
        // UpdateData();
    }
    public void UpdateData()
    {
        speechBubble.SetActive(true);
        motorcycleCount.text = parkinLot.allMotorcycle.Count.ToString() + "/" + parkinLot.motorcycleCapacity;
        motorAlCostText.text = GameManager.CaclText(parkinLot.motorCost.GetGold());
        motorcycleSpeedText.text = GameManager.CaclText(parkinLot.hiz) + " m/s";
        motorcycleSpeedCost.text = GameManager.CaclText(parkinLot.motorcycleSpeedCost.GetGold());
        motorcycleSpeedNext.text = "+" + GameManager.CaclText((parkinLot.hiz * (parkinLot.speedIncreasePercentage/100))) + " m/s";
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkinlotData : MonoBehaviour
{
    ParkinLot parkinLot;
    Level level;
    [SerializeField] Text motorSayisi
    ,motorAlCostText,motorHiziText,motorHiziTextCost;

    void Start()
    {
        parkinLot = GetComponentInParent<ParkinLot>();
        level= GetComponentInParent<Level>();
        UpdateData();
    }
    public void UpdateData()
    {
        motorSayisi.text = parkinLot.tumMotorlar.Count.ToString() + "/" + parkinLot.motorKapasitesi;
        motorAlCostText.text = GameManager.CaclText(parkinLot.motorCost.GetGold());
        // motorHiziText.text = parkinLot.tumMotorlar[0].agent.speed.ToString();
        motorHiziTextCost.text = GameManager.CaclText(parkinLot.motorHiziCost.GetGold());
    }
}

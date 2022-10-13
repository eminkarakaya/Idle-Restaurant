using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestorantData : MonoBehaviour
{
    Level level;
    Restaurant restaurant;
    [SerializeField] Text garsonYurumeHiziText,garsonYurumeHiziTextNext,musteriGelmeSikligiText
    ,musteriGelmeSikligiTextNext,garsonSayisiText,masaSayisiText,titleText
    ,garsonMoveCost,garsonAlmaCost,masaAlmaCost,musteriSiklikCost;

    void Start()
    {
        restaurant = GetComponentInParent<Restaurant>();
        level = GetComponentInParent<Level>();
        UpdateData();
    }
    public void UpdateData()
    {
        masaAlmaCost.text = GameManager.CaclText(restaurant.masaUcreti.GetGold());
        garsonAlmaCost.text = GameManager.CaclText(restaurant.garsonUcreti.GetGold());
        musteriSiklikCost.text = GameManager.CaclText(restaurant.musteriSiklikUcreti.GetGold());
        garsonMoveCost.text = GameManager.CaclText(restaurant.garsonMoveUcreti.GetGold());
        garsonSayisiText.text = "Garson Sayısı: " + restaurant.garsonSayisi+ "/" +restaurant.garsonKapasitesi;
        masaSayisiText.text = "Masa Sayısı: " + restaurant.masaSayisi + "/" + restaurant.masaKapasitesi;
        garsonYurumeHiziText.text = restaurant.tumGarsonlar[0].transform.GetChild(0).GetComponent<Garson>().moveSpeed.ToString();
        garsonYurumeHiziTextNext.text = restaurant.moveNext.ToString();
        musteriGelmeSikligiText.text = restaurant.GetComponentInChildren<CustomerCreator>().frequency.ToString();
        musteriGelmeSikligiTextNext.text = restaurant.frekansNext.ToString();
    }
}

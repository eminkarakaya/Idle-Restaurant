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
        Debug.Log(restaurant + " res");
        UpdateData();
    }
    public void UpdateData()
    {
        Debug.Log(masaAlmaCost + " " + restaurant.masaUcreti);
        masaAlmaCost.text = restaurant.masaUcreti.ToString();
        garsonAlmaCost.text = restaurant.garsonUcreti.ToString();
        musteriSiklikCost.text = restaurant.musteriSiklikUcreti.ToString();
        garsonMoveCost.text = restaurant.garsonMoveUcreti.ToString();
        garsonSayisiText.text = "Garson Sayısı: " + restaurant.garsonSayisi.ToString();
        masaSayisiText.text = "Masa Sayısı: " + restaurant.masaSayisi.ToString();
        garsonYurumeHiziText.text = restaurant.tumGarsonlar[0].transform.GetChild(0).GetComponent<Garson>().moveSpeed.ToString();
        garsonYurumeHiziTextNext.text = restaurant.moveNext.ToString();
        musteriGelmeSikligiText.text = restaurant.GetComponentInChildren<CustomerCreator>().frequency.ToString();
        musteriGelmeSikligiTextNext.text = restaurant.frekansNext.ToString();
    }
}

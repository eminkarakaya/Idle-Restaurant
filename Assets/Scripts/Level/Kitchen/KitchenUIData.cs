using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenUIData : MonoBehaviour
{
    Level level;
    Kitchen kitchen;
    [SerializeField] private Button buyOvenButton,buyCounterButton,buyPizzaCounterButton,buyChefButton;
    [SerializeField] Text firinSayisi,kasaSayisi
    ,counterSayisi,asciSayisi,counterAlCost,firinAlCost,asciAlCost,kasaAlCost;
    [SerializeField] TextMeshProUGUI pizzaCikmaSuresi;
    void Awake()
    {
        level = GetComponentInParent<Level>();
        kitchen = GetComponentInParent<Kitchen>();
    }
    public void UpdateData()
    {
        counterSayisi.text = kitchen.useablePizzaCounters.Count + "/" + kitchen.allPizzaCounters.Count;
        asciSayisi.text =  kitchen.cookCount + "/" + kitchen.chefCapacity;
        firinSayisi.text = kitchen.ovenCount + "/" + kitchen.allOven.Count;
        kasaSayisi.text =  kitchen.useableCounters.Count + "/" + kitchen.allCounters.Count;
        firinAlCost.text = GameManager.CaclText(kitchen.ovenCost.GetGold());
        asciAlCost.text = GameManager.CaclText(kitchen.asciCost.GetGold());
        kasaAlCost.text = GameManager.CaclText(kitchen.counterCost.GetGold());
        counterAlCost.text = GameManager.CaclText(kitchen.pizzaCounterCost.GetGold());
        if(!kitchen.isLocked)
            pizzaCikmaSuresi.text = GameManager.CaclText(kitchen.PizzaMakingTime())+" s";
        else
            pizzaCikmaSuresi.text = GameManager.CaclText(kitchen.unlockCost.GetGold())+"$";
    }
    public void ToggleChefButton(bool state)
    {
        buyChefButton.interactable = state;
    }
}

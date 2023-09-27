using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestaurantUIData : MonoBehaviour
{
    Level level;
    Restaurant restaurant;
    [SerializeField] private Button buyWaiterButton,buyWaiterSpeedButton,buyTableButton,buyCustomerFrequencyButton;
    [SerializeField] Text waiterMoveSpeedText,waiterMoveSpeedTextNext,customerFrequencyText
    ,customerFrequencyTextNext,waiterCountText,tableCountText,titleText
    ,waiterSpeedCost,waiterBuyCost,tableBuyCost,customerFrequencyCost;
    [SerializeField] TextMeshProUGUI pizzaDistributingTime;
    void Awake()
    {
        restaurant = GetComponentInParent<Restaurant>();
        level = GetComponentInParent<Level>();
    }
    void Start()
    {
        
    }
    public void UpdateData()
    {
        tableBuyCost.text = GameManager.CaclText(restaurant.tableCost.GetGold());
        waiterBuyCost.text = GameManager.CaclText(restaurant.waiterCost.GetGold());
        customerFrequencyCost.text = GameManager.CaclText(restaurant.customerFrequencyCost.GetGold());
        waiterSpeedCost.text = GameManager.CaclText(restaurant.waiterSpeedCost.GetGold());
        waiterCountText.text = "Garson Sayısı: " +  restaurant.customerCount+ "/" +restaurant.waiterCapacity;
        tableCountText.text = "Masa Sayısı: " + restaurant.tableCount + "/" + restaurant.tableCapacity;
        waiterMoveSpeedText.text = GameManager.CaclText(restaurant.moveSpeed);
        waiterMoveSpeedTextNext.text = GameManager.CaclText (restaurant.moveNext);
        customerFrequencyText.text = GameManager.CaclText(restaurant.GetComponentInChildren<CustomerCreator>().frequency);
        customerFrequencyTextNext.text = GameManager.CaclText(restaurant.frequencyNext);
        if(!restaurant.isLocked)
            pizzaDistributingTime.text = GameManager.CaclText(restaurant.PizzaDistributingTime()) + " s";
        else
            pizzaDistributingTime.text = GameManager.CaclText(restaurant.unlockCost.GetGold()) + "$";
    }
    public void ToggleWaiterButton(bool state)
    {
        buyWaiterButton.interactable = state;
    }
}

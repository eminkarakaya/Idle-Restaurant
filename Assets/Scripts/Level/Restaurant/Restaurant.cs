using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : Department 
{
    public int earnedMoneyFromCustomer;
    public List<Chair> dirtyPlates;
    public List<Chair> waitingForFoodChairs;
    public List<Counter> foodReadyCounters;   
    public int waiterCapacity;
    public List<Chair> emptyChairs;
    public List<Chair> allChairs;
    public RestorantData restaurantData;
    public List<GameObject> allWaiters;
    public List<GameObject> allTables;
    public List<Transform> waiterWaitPlace;
    public override Level level {get; set;}
    public override GameObject dataPanel { get; set; }
    public override Transform camPlace { get; set; }
    [SerializeField] private Transform _camTransform;
    [SerializeField] private GameObject _dataPanel;
    public Gold waiterCost;
    public Gold tableCost;
    public Gold waiterSpeedCost;
    public Gold customerFrequencyCost;
    [HideInInspector] public float frequencyDecreasePercentage = 3;
    [HideInInspector] public float moveSpeedPercentageIncrease = 4;
    [HideInInspector] public float frequencyNext;
    [HideInInspector] public float moveNext;
    public int tableCount =0;
    [HideInInspector]public int tableCapacity;
    public int customerCount = 0;
    public float moveSpeed = 2;
    void OnEnable()
    {
        
    }
    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        level = GetComponentInParent<Level>();
        restaurantData = GetComponentInChildren<RestorantData>();
        
    }
    void Start()
    {
        tableCapacity = allTables.Count;
        camPlace = _camTransform;
        selectableCollider = GetComponent<Collider>();
        dataPanel = _dataPanel;
    }
    public void BuyWaiter(bool isPaid)
    {
        if(waiterCapacity == customerCount)
        {
            return;
        }
        if(isPaid)
        {
            if(GameManager.instance.GetMoney() < waiterCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetMoney(-waiterCost.GetGold());
            }
        }
        customerCount ++;
        var waiter = Instantiate(levelManager.waiterPrefab,waiterWaitPlace[customerCount-1].position,Quaternion.identity);
        waiter.GetComponentInChildren<Garson>().waitingPlace = waiterWaitPlace[customerCount-1];
        waiter.GetComponentInChildren<Garson>().level = level;
        waiter.GetComponentInChildren<Garson>().moveSpeed = moveSpeed;
        waiterCost.SetGold(100);
        allWaiters.Add(waiter);
        restaurantData.UpdateData();
    }
    public void BuyTable(bool isPaid)
    {
        if(tableCount == tableCapacity)
            return;
        if(isPaid)
        {
            if(GameManager.instance.GetMoney() < tableCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetMoney(-tableCost.GetGold());
            }
        }

        tableCount ++;
        allTables[tableCount-1].gameObject.SetActive(true);
        emptyChairs.Add(allTables[tableCount-1].transform.GetChild(0).GetComponent<Chair>());
        emptyChairs.Add(allTables[tableCount-1].transform.GetChild(2).GetComponent<Chair>());
        allChairs.Add(allTables[tableCount-1].transform.GetChild(0).GetComponent<Chair>());
        allChairs.Add(allTables[tableCount-1].transform.GetChild(2).GetComponent<Chair>());
        tableCost.SetGold(100);
        restaurantData.UpdateData();
    }
    public void IncreaseMovementSpeed(bool isPaid)
    {
        if(isPaid)
        {
            if(GameManager.instance.GetMoney() < waiterSpeedCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetMoney(-waiterSpeedCost.GetGold());
            }
        }

        GameManager.instance.SetMoney(-waiterSpeedCost.GetGold());
        moveSpeed += moveSpeed * (moveSpeedPercentageIncrease/100);
        for (int i = 0; i < allWaiters.Count; i++)
        {
            allWaiters[i].transform.GetChild(0).GetComponent<Garson>().moveSpeed = moveSpeed;
        }
        moveNext = moveSpeed + moveSpeed * (moveSpeedPercentageIncrease/100);            
        waiterSpeedCost.SetGold(100);
        restaurantData.UpdateData();
    }
    public void MusteriSikligiArttir(bool isPaid)
    {
        if(isPaid)
        {
            if(GameManager.instance.GetMoney() < customerFrequencyCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetMoney(-customerFrequencyCost.GetGold());
            }
        }
        if(GameManager.instance.GetMoney() < customerFrequencyCost.GetGold())
            return;
        GameManager.instance.SetMoney(-customerFrequencyCost.GetGold());
        GetComponentInChildren<CustomerCreator>().frequency -= (GetComponentInChildren<CustomerCreator>().frequency * (frequencyDecreasePercentage/100));
        frequencyNext = GetComponentInChildren<CustomerCreator>().frequency - (GetComponentInChildren<CustomerCreator>().frequency * (frequencyDecreasePercentage/100));
        customerFrequencyCost.SetGold(100);
        restaurantData.UpdateData();
    }
    public void UnlockRestaurant()
    {
        if(unlockCost.GetGold() <= GameManager.instance.GetMoney())
        {
            if (level.restoranTask.activeInHierarchy == true)
                level.restoranTask.SetActive(false);
            GameManager.instance.SetMoney(-unlockCost.GetGold());
            lockedPanel.SetActive(false);
            BuyTable(false);
            BuyWaiter(false);
            isLocked = false;
            @lock.SetActive(false);
            restaurantData.UpdateData();
            level.IsRestaurantReady(false);
            SelectManager.instance.GeriButonu();
        }
    }
    public float PizzaDistributingTime()
    {
        float tableDistanceAverage =0;
        for (int i = 0; i < allChairs.Count; i++)
        {
            tableDistanceAverage += Vector3.Distance (waiterWaitPlace[0].position,allChairs[i].transform.position);
        }
        tableDistanceAverage = (tableDistanceAverage / allChairs.Count)/moveSpeed;
        float dishCounterAverageDistance =0;
        var temp = 0;
        for (int i = 0; i < level.scullery.Count; i++)
        {
            for (int j = 0; j < level.scullery[i].currentCounters.Count; j++)
            {
                dishCounterAverageDistance += Vector3.Distance(waiterWaitPlace[0].position,level.scullery[i].currentCounters[j].transform.position);
                temp++;
            }
        }
        var temp2 = 0;
        dishCounterAverageDistance = (dishCounterAverageDistance / temp)/moveSpeed;
        float counterAverageDistance = 0;
        for (int i = 0; i < level.kitchens.Length; i++)
        {
                
            for (int j = 0; j < level.kitchens[i].useableCounters.Count; j++)
            {
                counterAverageDistance += Vector3.Distance(waiterWaitPlace[0].position,level.kitchens[i].useableCounters[j].transform.position);
                temp2 ++;
            }
        }
        counterAverageDistance = (counterAverageDistance / temp2)/moveSpeed;
        return (counterAverageDistance + dishCounterAverageDistance + tableDistanceAverage) / allWaiters.Count;
        
    }
}

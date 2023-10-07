using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takeaway : Department
{
    public int takeawayIndex;
    [SerializeField] public Kitchen kitchen;
    public Item queue;
    TakeawayUIData takeawayUIData;
    ParkingLotData takeawayData;
    public Transform spawnPoint;
    public override Level level {get; set;}
    [SerializeField] private GameObject _dataPanel;
    [SerializeField] private Transform _camPlace;
    public override GameObject dataPanel { get; set; }
    public override Transform camPlace { get; set; }
    public List<Counter> foodReadyCounters;
    public int motorcycleCapacity;
    public List<Motorcycle> allMotorcycle;    
    public Transform startingPos;
    public Transform finishPos;
    public Gold motorCost;
    public Gold motorcycleSpeedCost;
    public float speed = 3;
    public int motorcycleCount;
    [HideInInspector] public float speedIncreasePercentage = 4;
    void Awake()
    {
        level = GetComponentInParent<Level>();
        levelManager = FindObjectOfType<LevelManager>();
        takeawayUIData = GetComponentInChildren<TakeawayUIData>();
    }
    void Start()
    {
        selectableCollider = GetComponent<Collider>();
        dataPanel = _dataPanel;
        camPlace = _camPlace;

    }
    public void SaveParkinglot()
    {
        takeawayData = new ParkingLotData{
            motorcycleSpeed = speed,
            motorcycleCost = motorCost.GetGold(),
            motorcycleCount = motorcycleCount,
            motorcycleSpeedCost = motorcycleSpeedCost.GetGold()
        };
        level.levelData.parkingLotData[takeawayIndex] = takeawayData;
    }
    public void LoadParkingLot()
    {
        
        if(level.levelData.parkingLotData[takeawayIndex] != null)
        {
            @lock.SetActive(false);
            takeawayData = level.levelData.parkingLotData[takeawayIndex];
            for (int i = 0; i < takeawayData.motorcycleCount; i++)
            {
                BuyMotorcycle(false);
            }
            for (int i = 0; i < allMotorcycle.Count; i++)
            {
                allMotorcycle[i].agent.speed = takeawayData.motorcycleSpeed;
            }
            speed = takeawayData.motorcycleSpeed;
            motorCost.SetGold(takeawayData.motorcycleCost);
            motorcycleSpeedCost.SetGold(takeawayData.motorcycleSpeedCost);
            motorcycleCount = takeawayData.motorcycleCount;
            takeawayUIData.UpdateData();
        }
        else
        {
            takeawayData = new ParkingLotData();
        }
    }
    public void BuyMotorcycle(bool isPaid)
    {
        if(motorcycleCount == motorcycleCapacity)
            return;
        if(isPaid)
        {
            if(GameManager.instance.GetMoney() < motorCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetMoney(-motorCost.GetGold());
            }
        }

        motorcycleCount++;
        var motor = Instantiate(levelManager.motorcyclePrefab,spawnPoint.position,Quaternion.identity);
        allMotorcycle.Add(motor.GetComponent<Motorcycle>());
        motor.GetComponent<Motorcycle>().level = level;
        motor.GetComponent<Motorcycle>().parkinLot = this;
        motor.GetComponent<Motorcycle>().sira = queue;
        motor.GetComponent<Motorcycle>().agent.speed = speed;
        GameManager.instance.SetIdleMoneyText(level.CalculateEarnedMoneyOfPerSeconds());
        takeawayUIData.UpdateData();
    }
    public void IncreaseMotorcycleSpeed(bool isPaid)
    {
        if(isPaid)
        {
            if(GameManager.instance.GetMoney() < motorcycleSpeedCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetMoney(-motorcycleSpeedCost.GetGold());
            }
        }
        speed += speed * (speedIncreasePercentage/100);
        for (int i = 0; i < allMotorcycle.Count; i++)
        {
            allMotorcycle[i].agent.speed = speed;            
        }
        GameManager.instance.SetIdleMoneyText(level.CalculateEarnedMoneyOfPerSeconds());
        takeawayUIData.UpdateData();
    }
    public void UnlockTakeaway()
    {
        if(unlockCost.GetGold() <= GameManager.instance.GetMoney() && level.IsRestaurantReady())
        {

            BuyMotorcycle(false);
            isLocked = false;
            @lock.SetActive(false);
            takeawayUIData.UpdateData();
            GameManager.instance.SetMoney(-kitchen.unlockCost.GetGold());
            if(kitchen.isLocked)
                kitchen.UnLock(false);
            SelectManager.instance.BackButton();
            
        }
    }
}

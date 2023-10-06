using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkinLot : Department
{
    public int parkingLotIndex;
    [SerializeField] public Kitchen kitchen;
    public Item queue;
    ParkinglotUIData parkingLotUIData;
    ParkingLotData parkingLotData;
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
    public float hiz = 3;
    public int motorcycleCount;
    [HideInInspector] public float speedIncreasePercentage = 4;
    void Awake()
    {
        level = GetComponentInParent<Level>();
        levelManager = FindObjectOfType<LevelManager>();
        parkingLotUIData = GetComponentInChildren<ParkinglotUIData>();
    }
    void Start()
    {
        selectableCollider = GetComponent<Collider>();
        dataPanel = _dataPanel;
        camPlace = _camPlace;

    }
    public void SaveParkinglot()
    {
        parkingLotData = new ParkingLotData{
            motorcycleSpeed = hiz,
            motorcycleCost = motorCost.GetGold(),
            motorcycleCount = motorcycleCount,
            motorcycleSpeedCost = motorcycleSpeedCost.GetGold()
        };
        level.levelData.parkingLotData[parkingLotIndex] = parkingLotData;
    }
    public void LoadParkingLot()
    {
        
        if(level.levelData.parkingLotData[parkingLotIndex] != null)
        {
            @lock.SetActive(false);
            parkingLotData = level.levelData.parkingLotData[parkingLotIndex];
            for (int i = 0; i < parkingLotData.motorcycleCount; i++)
            {
                MotorcycleAl(false);
            }
            for (int i = 0; i < allMotorcycle.Count; i++)
            {
                allMotorcycle[i].agent.speed = parkingLotData.motorcycleSpeed;
            }
            hiz = parkingLotData.motorcycleSpeed;
            motorCost.SetGold(parkingLotData.motorcycleCost);
            motorcycleSpeedCost.SetGold(parkingLotData.motorcycleSpeedCost);
            motorcycleCount = parkingLotData.motorcycleCount;
            parkingLotUIData.UpdateData();
        }
        else
        {
            parkingLotData = new ParkingLotData();
        }
    }
    public void MotorcycleAl(bool isPaid)
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
        motor.GetComponent<Motorcycle>().agent.speed = hiz;
        GameManager.instance.SetIdleMoneyText(level.CalculateEarnedMoneyOfPerSeconds());
        parkingLotUIData.UpdateData();
    }
    public void MotorHiziArttir(bool isPaid)
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
        hiz += hiz * (speedIncreasePercentage/100);
        for (int i = 0; i < allMotorcycle.Count; i++)
        {
            allMotorcycle[i].agent.speed = hiz;            
        }
        GameManager.instance.SetIdleMoneyText(level.CalculateEarnedMoneyOfPerSeconds());
        parkingLotUIData.UpdateData();
    }
    public void UnlockTakeaway()
    {
        if(unlockCost.GetGold() <= GameManager.instance.GetMoney() && level.IsRestaurantReady())
        {

            MotorcycleAl(false);
            isLocked = false;
            @lock.SetActive(false);
            parkingLotUIData.UpdateData();
            GameManager.instance.SetMoney(-kitchen.unlockCost.GetGold());
            if(kitchen.isLocked)
                kitchen.UnLock(false);
            SelectManager.instance.BackButton();
            
        }
    }
}

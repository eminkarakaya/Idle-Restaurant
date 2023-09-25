using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData 
{
    public bool isFirst;
    public int lastSceneIndex;
    public int money;
    public LevelData[] levelData;
    public GameData()
    {
        levelData = new LevelData[10];
        isFirst = true;
        lastSceneIndex = 0;
        money = 10000;
    }
}
[System.Serializable]
public class LevelData 
{
    public bool task1, task2, task3;
    public float goldEarnedPerSec;
    public string lastLoginDate;
    public bool isUnlock;
    public SculleryData[] sculleryData;
    public ParkingLotData[] parkingLotData;
    public KitchenData[] kitchenData;
    public RestaurantData restaurantData;
    public LevelData(int sculleriesCount,int parkingLotCount,int kitchenCount)
    {
        sculleryData = new SculleryData[sculleriesCount];
        parkingLotData = new ParkingLotData[parkingLotCount];
        kitchenData = new KitchenData[kitchenCount];
    }
}
[System.Serializable]
public class SculleryData
{
    public bool sculleryIsLocked = true;
    public int dishwasherCount;
    public int sinkCount;
    public int dishCounterCount;
    public int dishwasherCost;
    public int sinkCost;
    public int dishCounterCost;
}
[System.Serializable]
public class ParkingLotData
{   
    public float motorcycleSpeed;
    public int motorcycleCount;
    public int motorcycleCost;
    public int motorcycleSpeedCost;
    

}
[System.Serializable]
public class KitchenData
{
    
    public bool kitchenIsLocked = true;
    public int ovenCount;
    public int counterCount;
    public int pizzaCounterCount;
    public int chefCount;
    public int chefCost;
    public int ovenCost;
    public int counterCost;
    public int pizzaCounterCost;
}
[System.Serializable]
public class RestaurantData
{
    public bool restaurantIsLocked = true;
    public int tableCount;
    public int waiterCount;
    public float customerFrequency = 2;
    public float waiterSpeed = 2;
    public int waiterCost;
    public int tableCost;
    public int waiterSpeedCost;
    public int customerFrequencyCost;   
}
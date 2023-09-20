using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelData
{
    public bool task1, task2, task3;
    public int customerCount;
    public float goldEarnedPerSec;
    public string lastLoginDate;
    public int levelIndex;
    public bool isUnlock;
    public int kitchenCount = 0;
    public int parkinLotCount = 0;

    public float[] motorcycleSpeed = new float[10];
    public int[] motorcycleCount = new int[10];
    public bool[] kitchenIsLocked = new bool[10];
    public bool restaurantIsLock = true;
    public int [] ovenCount = new int[10];
    public int [] counterCount = new int[10];
    public int [] pizzaCounterCount = new int[10];
    public int [] chefCount = new int[10];
    public int [] chefCost = new int[10];
    public int [] ovenCost = new int[10];
    public int [] counterCost = new int[10];
    public int [] pizzaCounterCost = new int[10];
    public int [] kitchenIndexes = new int[10];
    public int [] dishwasherCount = new int[10];
    public int [] sinkCount = new int[10];
    public int [] dishCounterCount = new int[10];
    public bool [] sculleryIsLocked = new bool [10];
    public Vector3 [] customerLocations;
    public CustomerBaseState [] musteriState;
    public Vector3 [] waiterLocations;
    public Vector3 [,] chefLocations;
    public Vector3 [,] bulasikciLocations;
    public int tableCount;
    public int waiterCount;
    public float waiterSpeed;
    public int waiterCost;
    public int tableCost;
    public int waiterSpeedCost;
    public int customerFrequencyCost;
    public int sculleryCount;
    
    public LevelData(Level level)
    {
        task1 = false;
        task2 = false;
        task3 = false;
        waiterSpeed = 2;
        motorcycleSpeed = new float[10];
        motorcycleCount = new int[10];
        kitchenIsLocked = new bool [10];
        ovenCount = new int[10];
        counterCount = new int[10];
        pizzaCounterCount = new int[10];
        chefCount = new int[10];
        chefCost = new int[10];
        ovenCost = new int[10];
        counterCost = new int[10];
        pizzaCounterCost = new int[10];
        kitchenIndexes = new int[10];
        dishwasherCount = new int[10];
        sinkCount = new int[10];
        dishCounterCount = new int[10];
        sculleryIsLocked = new bool [10];
        levelIndex = level.levelIndex;
        isUnlock = level.isUnlock;
        kitchenCount = level.kitchens.Length;
    //      public Vector3 [] customerLocations;
        for (int i = 0; i < level.kitchens.Length; i++)
        {
            kitchenIsLocked[level.levelIndex] = level.kitchens[i].isLocked;
            kitchenIndexes[level.levelIndex] = (level.kitchens[i].kitchenIndex);
            ovenCount[level.levelIndex] = (level.kitchens[i].ovenCount);
            counterCount[level.levelIndex] = (level.kitchens[i].counterCount);
            pizzaCounterCount[level.levelIndex] = (level.kitchens[i].pizzaCounterCount);
            chefCount[level.levelIndex] = (level.kitchens[i].cookCount);
            kitchenIsLocked[level.levelIndex] = (level.kitchens[i].isLocked);
            chefLocations = new Vector3[level.kitchens.Length,level.kitchens[i].allChefs.Count];   
        }
        for (int i = 0; i < level.scullery.Count; i++)
        {
            sinkCount[level.levelIndex] = level.scullery[i].sinkCount;
            dishCounterCount[level.levelIndex] = level.scullery[i].dishCounterCount;
            sculleryIsLocked[level.levelIndex] = level.scullery[i].isLocked;
            dishwasherCount[level.levelIndex] = level.scullery[i].dishwasherCount;
            bulasikciLocations = new Vector3[level.scullery.Count,level.scullery[i].dishwasherCount];
        }
        tableCount = level.restaurant.allTables.Count;
        waiterCount = level.restaurant.allWaiters.Count;
        waiterLocations = new Vector3[waiterCount];
        
        waiterCost = level.restaurant.waiterCost.GetGold();
        tableCost = level.restaurant.tableCost.GetGold();
        waiterSpeedCost = level.restaurant.waiterSpeedCost.GetGold();
        customerFrequencyCost = level.restaurant.customerFrequencyCost.GetGold();
        customerCount = 0;
        customerLocations = new Vector3[customerCount];
        
    }
}


[System.Serializable]
public class GameData 
{
    public bool isFirst;
    public int lastSceneIndex;
    public int para;
    public LevelData [] levelData = new LevelData[10];
    public GameData()
    {
        isFirst = true;
        lastSceneIndex = 0;
        para = 10000;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scullery : Department
{
    Restaurant restaurant;
    SculleryData sculleryData;
    public override Level level {get; set;}
    public override GameObject dataPanel { get; set; }
    public override Transform camPlace { get; set; }
    [Header("Panels")]
    [SerializeField] GameObject _dataPanel;
    [SerializeField] Transform _camPlace;
    [Header("Lists")]
    public List<SculleryCounter> allDishCounter;
    public List<SculleryCounter> currentDishCounters;
    public List<Sink> allSinks;
    public List<Sink> currentSinks;
    public List<DishWasher> allDishwasher;
    public int dishwasherCapacity = 3;
    [Header("Costs")]
    public Gold dishCounterCost;
    public Gold sinkCost;
    public Gold dishwasherCost;
    public Transform dishwasherSpawn;
    [Header("Data")]
    [SerializeField] private int sculleryIndex;
    public SculleryUIData sculleryUIData;
    public int sinkCount,dishCounterCount,dishwasherCount;
    public float washingTime = 2f;
    void Awake()
    {
        level = GetComponentInParent<Level>();
        restaurant = FindObjectOfType<Restaurant>();
        selectableCollider = GetComponent<Collider>();
        dataPanel = _dataPanel;
        camPlace = _camPlace;
        levelManager = FindObjectOfType<LevelManager>();
        sculleryUIData = GetComponentInChildren<SculleryUIData>();
        allSinks = GetComponentsInChildren<Sink>().ToList();
        allDishCounter = GetComponentsInChildren<SculleryCounter>().ToList();
    }
    
    private void Start() {
        
    }
    public void SaveScullery()
    {
        sculleryData = new SculleryData
        {
            sculleryIsLocked = isLocked,
            sinkCount = sinkCount,
            dishCounterCount = dishwasherCount,
            dishwasherCount = dishCounterCount,
            dishCounterCost = dishCounterCost.GetGold(),
            sinkCost = sinkCost.GetGold(),
            dishwasherCost = dishwasherCost.GetGold(),
        };
        level.levelData.sculleryData[sculleryIndex] = sculleryData;
    }
    public void LoadScullery() 
    {
        if(level.levelData.sculleryData[sculleryIndex] != null)
        {
            sculleryData = level.levelData.sculleryData[sculleryIndex];
            isLocked =sculleryData.sculleryIsLocked; 
            if(!isLocked)
            {
                level.unlockedSculleries.Add(this);
                @lock.SetActive(false);
                for (int i = 0; i < sculleryData.dishCounterCount; i++)
                {
                    BulasikCounterSatinAl(false);
                }
                for (int i = 0; i < sculleryData.sinkCount; i++)
                {
                    SinkSatinAl(false);
                }
                for (int i = 0; i < sculleryData.dishwasherCount; i++)
                {
                    BulasikciSatinAl(false);
                }
            }
        }
        else
        {
            sculleryData = new SculleryData();
        }
        dishCounterCost.SetGold(sculleryData.dishCounterCost);
        sinkCost.SetGold(sculleryData.sinkCost);
        dishwasherCost.SetGold(sculleryData.dishCounterCost);
        sculleryUIData.UpdateData();
    }
   
    public SculleryCounter FindEmptyDishCounter()
    {
        for (int i = 0; i < currentDishCounters.Count; i++)
        {
            if(currentDishCounters[i].plates.Count == 0)
                return currentDishCounters[i];
        }
        return currentDishCounters[0];
    }
   
    public SculleryCounter FindDishCounter()
    {
        
        var least = currentDishCounters[0];
        for (int i = 0; i < currentDishCounters.Count; i++)
        {
            if(currentDishCounters[i].plates.Count < least.plates.Count)
            {
                least = currentDishCounters[i];
            }
        }
        return least;
    }
    public SculleryCounter FindCounterWithMostDishwasher()
    {
        var enCok = currentDishCounters[0];
        for (int i = 0; i < currentDishCounters.Count; i++)
        {
            if(currentDishCounters[i].dishwashers.Count > enCok.dishwashers.Count)
            {
                enCok = currentDishCounters[i];
            }
        }
        return enCok;
    }
    public Sink FindSinkWithMostDishwasher()
    {
        var enCok = currentSinks[0];
        for (int i = 0; i < currentSinks.Count; i++)
        {
            if(currentSinks[i].dishwashers.Count == 1)
                continue;
            if(currentSinks[i].dishwashers.Count > enCok.dishwashers.Count)
            {
                enCok = currentSinks[i];
            }
        }
        return enCok;
    }
    
    public SculleryCounter GetEmptyBulasikCounter()
    {
        var enAz = currentDishCounters[0];
        for (int i = 0; i < currentDishCounters.Count; i++)
        {
            if(currentDishCounters[i].dishwashers.Count < enAz.dishwashers.Count) 
            {
                enAz = currentDishCounters[i];
            }
        }
    
        return enAz;
    }
    public Sink GetEmptySink()
    {
        
        Sink enAz = currentSinks[0];
        for (int i = 0; i < currentSinks.Count; i++)
        {
            if(currentSinks[i].dishwashers.Count < enAz.dishwashers.Count)
            {
                enAz = currentSinks[i];
            }
        }
        return enAz;
    }
    public void BulasikciSatinAl(bool isPaid)
    {
        if(allDishwasher.Count > dishwasherCapacity)
        {
            return;
        }
        if(isPaid)
        {
            if(GameManager.instance.GetMoney()< dishwasherCost.GetGold())
                return;
            else
            {
                GameManager.instance.SetMoney(-dishwasherCost.GetGold());
            }
        }
        dishwasherCount ++;
        var dishwasher = Instantiate(levelManager.dishwasherPrefab,dishwasherSpawn.position,Quaternion.identity);
        var dishwasherClass = dishwasher.transform.GetChild(0).GetComponent<DishWasher>();

        var counter = GetEmptyBulasikCounter();
        dishwasherClass.dishCounter = counter;
        allDishwasher.Add(dishwasherClass);
        counter.dishwashers.Add(dishwasher);
    
        var sink = GetEmptySink();
        dishwasherClass.sink = sink;   
        sink.dishwashers.Add(dishwasher);

        dishwasherClass.level = level;
        dishwasherClass.scullery = this;
        dishwasherCost.IncreaseGold(100);
        restaurant.CheckWaiterButton();
        sculleryUIData.UpdateData();

    }
    public void BulasikCounterSatinAl(bool isPaid)
    {
        if(currentDishCounters.Count == allDishCounter.Count)
            return;
        if(isPaid)
        {
            if(GameManager.instance.GetMoney()< dishCounterCost.GetGold())
                return;
            else
            {
                GameManager.instance.SetMoney(-dishCounterCost.GetGold());
            }
        }
        dishCounterCount++;
        var counter = allDishCounter[currentDishCounters.Count];
        currentDishCounters.Add(counter);
        counter.barrier.SetActive(false);
        var most = FindCounterWithMostDishwasher();
        if(most.dishwashers.Count == 0 || most.dishwashers.Count == 1)
        {
            return;
        }
        // dıger dishcounter ın queue sından cıkarmak lazım 

        DishWasher dishwasher = most.dishwashers[most.dishwashers.Count-1].transform.GetChild(0).GetComponent<DishWasher>(); 
        if(most.queue.Contains(dishwasher))
        {
            most.queue.Remove(dishwasher);
        }
        dishwasher.dishCounter = counter;
        dishwasher.currState = dishwasher.takePlateState;
        counter.dishwashers.Add(most.dishwashers[most.dishwashers.Count-1]);
        
        // var dishwasher = most.dishwashers[most.dishwashers.Count-1].transform.GetChild(0).GetComponent<DishWasher>();
        // if((dishwasher.currState == dishwasher.queueState || dishwasher.currState == dishwasher.queueWaitState) && dishwasher.queueState.previousState == dishwasher.takePlateState)
        // {
        //     // if(encok.queue.Contains(bulasikci))
        //     //     encok.queue.Remove(bulasikci);
        //     dishwasher.currState = dishwasher.takePlateState;
        // }
        // if(dishwasher.currState == dishwasher.waitPlateState)
        // {
        //     dishwasher.currState = dishwasher.takePlateState;
        // }
        most.dishwashers.Remove(most.dishwashers[most.dishwashers.Count-1]);
        dishCounterCost.IncreaseGold(100);
        restaurant.CheckWaiterButton();
        GameManager.instance.SetIdleMoneyText(level.CalculateEarnedMoneyOfPerSeconds());
        sculleryUIData.UpdateData();
    }
    public void SinkSatinAl(bool isPaid)
    {
        if(currentSinks.Count == allSinks.Count)
            return;
        if(isPaid)
        {
            if(GameManager.instance.GetMoney()< sinkCost.GetGold())
                return;
            else
            {
                GameManager.instance.SetMoney(-sinkCost.GetGold());
            }
        }
        sinkCount++;
        var sink = allSinks[currentSinks.Count];
        sink.gameObject.SetActive(true);
        currentSinks.Add(sink);
        var most = FindSinkWithMostDishwasher();
        if(most.dishwashers.Count == 0 ||most.dishwashers.Count == 1)
        {
            
        }
        else
        {
            most.dishwashers[most.dishwashers.Count-1].transform.GetChild(0).GetComponent<DishWasher>().sink = sink;
            sink.dishwashers.Add(most.dishwashers[most.dishwashers.Count-1]);
            var dishwasher = most.dishwashers[most.dishwashers.Count-1].transform.GetChild(0).GetComponent<DishWasher>();
            if((dishwasher.currState == dishwasher.queueState || dishwasher.currState == dishwasher.queueWaitState) && dishwasher.queueState.previousState == dishwasher.waitSinkState)
            {
                dishwasher.currState = dishwasher.putPlateState;
            }
            most.dishwashers.Remove(most.dishwashers[most.dishwashers.Count-1]);
        }
        sinkCost.IncreaseGold(100);
        GameManager.instance.SetIdleMoneyText(level.CalculateEarnedMoneyOfPerSeconds());
        sculleryUIData.UpdateData();
    }
    // KILIDINI ACINCA
    public void UnlockScullery()
    {
        if(unlockCost.GetGold() <= GameManager.instance.GetMoney())
        {
            //if (level.bulasikhaneTask.activeInHierarchy == true)
            //    level.bulasikhaneTask.SetActive(false);
            GameManager.instance.SetMoney(-unlockCost.GetGold());
            lockedPanel.SetActive (false);
            isLocked = false;
            @lock.SetActive(false);
            SinkSatinAl(false);
            BulasikCounterSatinAl(false);
            BulasikciSatinAl(false);
            sculleryUIData.UpdateData();
            level.RestaurantReady(false);
            level.unlockedSculleries.Add(this);
            restaurant.CheckWaiterButton();
            GameManager.instance.SetIdleMoneyText(level.CalculateEarnedMoneyOfPerSeconds());
            SelectManager.instance.BackButton();
        }
    }
    // YIKAMA SURESI HESAPLAMA
    public float PizzaMakingTime()
    {
        if (allDishwasher.Count == 0)
            return 0;
        float totalTime = 0f;
        for (int i = 0; i < allDishwasher.Count; i++)
        {
            totalTime += Vector3.Distance(allDishwasher[i].sink.transform.position,allDishwasher[i].dishCounter.transform.position);
            totalTime += Vector3.Distance(allDishwasher[i].sink.transform.position,allDishwasher[i].dishCounter.transform.position);
            totalTime += washingTime;
        }
        totalTime /= allDishwasher.Count;

        return totalTime/allDishwasher.Count;
    }
    
}

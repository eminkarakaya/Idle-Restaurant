using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scullery : Department
{
    public SculleryData sculleryData;
    public override Level level {get; set;}
    public override GameObject dataPanel { get; set; }
    public override Transform camPlace { get; set; }
    [SerializeField] GameObject _dataPanel;
    [SerializeField] Transform _camPlace;
    public List<SculleryCounter> allDishCounter;
    public List<SculleryCounter> currentCounters;
    public List<Sink> allSinks;
    public List<Sink> currentSinks;
    public List<DishWasher> allDishwasher;
    public int dishwasherCapacity = 3;
    public Gold dishCounterCost;
    public Gold sinkCost;
    public Gold dishwasherCost;
    public Transform dishwasherSpawn;
    public int sinkCount;
    public int dishwasherCount;
    public int dishCounterCount;
    void Awake()
    {
        level = GetComponentInParent<Level>();
        selectableCollider = GetComponent<Collider>();
        dataPanel = _dataPanel;
        camPlace = _camPlace;
        levelManager = FindObjectOfType<LevelManager>();
        sculleryData = GetComponentInChildren<SculleryData>();
    }
    public SculleryCounter FindEmptyDishCounter()
    {
        for (int i = 0; i < currentCounters.Count; i++)
        {
            if(currentCounters[i].plates.Count == 0)
                return currentCounters[i];
        }
        return currentCounters[0];
    }
    public SculleryCounter FindDishCounter()
    {
        var least = currentCounters[0];
        for (int i = 0; i < currentCounters.Count; i++)
        {
            if(currentCounters[i].dishwashers .Count == 0)
            {
                continue;
            }
            if(currentCounters[i].plates.Count < least.plates.Count)
            {
                least = currentCounters[i];
            }
        }
        return least;
    }
    public SculleryCounter FindCounterWithMostDishwasher()
    {
        var enCok = currentCounters[0];
        for (int i = 0; i < currentCounters.Count; i++)
        {
            if(currentCounters[i].dishwashers.Count > enCok.dishwashers.Count)
            {
                enCok = currentCounters[i];
            }
        }
        return enCok;
    }
    public Sink FindSinkWithMostDishwasher()
    {
        var enCok = currentSinks[0];
        for (int i = 0; i < currentSinks.Count; i++)
        {
            if(currentSinks[i].dishwashers.Count > enCok.dishwashers.Count)
            {
                enCok = currentSinks[i];
            }
        }
        return enCok;
    }
    
    public SculleryCounter GetEmptyBulasikCounter()
    {
        var enAz = currentCounters[0];
        for (int i = 0; i < currentCounters.Count; i++)
        {
            if(currentCounters[i].dishwashers.Count < enAz.dishwashers.Count)
            {
                enAz = currentCounters[i];
            }
        }
    
        return enAz;
    }
    public Sink GetEmptySink()
    {
        var enAz = currentSinks[0];
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
        dishwasherCost.SetGold(100);
        sculleryData.UpdateData();

    }
    public void BulasikCounterSatinAl(bool isPaid)
    {
        if(currentCounters.Count == allDishCounter.Count)
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
        var counter = allDishCounter[currentCounters.Count];
        currentCounters.Add(counter);
        counter.barrier.SetActive(false);
        var most = FindCounterWithMostDishwasher();
        if(most.dishwashers.Count == 0)
        {
            return;
        }
        most.dishwashers[most.dishwashers.Count-1].transform.GetChild(0).GetComponent<DishWasher>().dishCounter = counter;
        counter.dishwashers.Add(most.dishwashers[most.dishwashers.Count-1]);
        
        var dishwasher = most.dishwashers[most.dishwashers.Count-1].transform.GetChild(0).GetComponent<DishWasher>();
        if((dishwasher.currState == dishwasher.queueState || dishwasher.currState == dishwasher.queueWaitState) && dishwasher.queueState.previousState == dishwasher.takePlateState)
        {
            // if(encok.queue.Contains(bulasikci))
            //     encok.queue.Remove(bulasikci);
            dishwasher.currState = dishwasher.takePlateState;
        }
        if(dishwasher.currState == dishwasher.waitPlateState)
        {
            dishwasher.currState = dishwasher.takePlateState;
        }
        most.dishwashers.Remove(most.dishwashers[most.dishwashers.Count-1]);
        dishCounterCost.SetGold(100);
        sculleryData.UpdateData();
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
        if(most.dishwashers.Count == 0)
        {
            return;
        }
        most.dishwashers[most.dishwashers.Count-1].transform.GetChild(0).GetComponent<DishWasher>().sink = sink;
        sink.dishwashers.Add(most.dishwashers[most.dishwashers.Count-1]);
        var dishwasher = most.dishwashers[most.dishwashers.Count-1].transform.GetChild(0).GetComponent<DishWasher>();
        if((dishwasher.currState == dishwasher.queueState || dishwasher.currState == dishwasher.queueWaitState) && dishwasher.queueState.previousState == dishwasher.waitSinkState)
        {
            dishwasher.currState = dishwasher.putPlateState;
        }
        most.dishwashers.Remove(most.dishwashers[most.dishwashers.Count-1]);
        sinkCost.SetGold(100);
        sculleryData.UpdateData();
    }
    public void UnlockScullery()
    {
        if(unlockCost.GetGold() <= GameManager.instance.GetMoney())
        {
            //if (level.bulasikhaneTask.activeInHierarchy == true)
            //    level.bulasikhaneTask.SetActive(false);
            GameManager.instance.SetMoney(-unlockCost.GetGold());
            lockedPanel.SetActive(false);
            isLocked = false;
            @lock.SetActive(false);
            SinkSatinAl(false);
            BulasikCounterSatinAl(false);
            BulasikciSatinAl(false);
            sculleryData.UpdateData();
            level.IsRestaurantReady(false);
            SelectManager.instance.BackButton();
        }
    }
    public float PizzaMakingTime()
    {
        if (allDishwasher.Count == 0)
            return 0;
        float totalTime = 0f;
        for (int i = 0; i < allDishwasher.Count; i++)
        {
            totalTime += Vector3.Distance(allDishwasher[i].sink.transform.position,allDishwasher[i].dishCounter.transform.position);
            totalTime += Vector3.Distance(allDishwasher[i].sink.transform.position,allDishwasher[i].dishCounter.transform.position);
            totalTime += allDishwasher[i].washState.washingTime;
        }
        totalTime /= allDishwasher.Count;

        return totalTime/allDishwasher.Count;
    }

}

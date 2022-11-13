using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    public GameObject bulasikhaneTask, mutfakTask, restoranTask;
    public Sprite orderSprite;
    public GameObject dough;
    public GameObject pizza;
    public GameObject idleMoneyCanvas;
    public TextMeshProUGUI earnedIdleMoneyText;
    public TextMeshProUGUI passedTimeText;
    public string lastLoginDate;
    public LevelData data;
    public LevelManager levelManager;
    private int _gold;
    private TextMeshProUGUI goldText;
    public int levelIndex;
    public bool isUnlock;
    public Restaurant restaurant;
    public Kitchen[] kitchens;
    public List<Bulasikhane> scullery;
    public List <ParkinLot> parkinLot;

    void OnApplicationQuit()
    {
        Save();
    }
    void Awake()
    {
        restaurant = GetComponentInChildren<Restaurant>();
    }
    void Start()
    {
        kitchens = FindObjectsOfType<Kitchen>();
        levelManager = FindObjectOfType<LevelManager>();
        if (PlayerPrefs.HasKey("data"))
            LoadLevel();
        var totalDishwasher =0;
        for (int i = 0; i < scullery.Count; i++)
        {
            totalDishwasher += scullery[i].dishwasherCount;
        }
        var totalChef = 0;
        for (int i = 0; i < kitchens.Length; i++)
        {
            if(kitchens[i].kuryeMutfagimi)
                continue;
            totalChef += kitchens[i].cookCount;
        }
        if(totalDishwasher == 0 ||restaurant.isLocked || totalChef == 0)
        {
            idleMoneyCanvas.SetActive(false);
        }

        earnedIdleMoneyText.text = GameManager.CaclText((CalcPassingTime()*(CalculateEarnedMoneyOfPerSeconds())/10)) + "$";
        TimeSpan t = TimeSpan.FromSeconds( CalcPassingTime() );
        string str;
        if(CalcPassingTime() > 86400)
        {
            str = t.ToString(@"dd\hh\:mm\:ss\:fff");
        }
        else if(CalcPassingTime() > 3600)
        {
            str = t.ToString(@"hh\:mm\:ss\:");
        }
        else if(CalcPassingTime() > 60)
        {
            str = t.ToString(@"mm") + " dk " + t.ToString(@"ss") + " sn";
        }
        else
        {
            str = t.ToString(@"ss") + " sn";
        }

        passedTimeText.text = str;
        IsRestaurantReady(true);
    }

    public void SetGold(int value)
    {
        _gold+= value;
        goldText.text = _gold.ToString();
    }
    public int GetGold()
    {
        return _gold;
    }
    public void Save()
    {
        data = new LevelData(this);
        GameManager.instance.gameData.levelData[levelIndex] = new LevelData(this);
        GameManager.instance.gameData.levelData[levelIndex].isUnlock = isUnlock;
        GameManager.instance.gameData.para = GameManager.instance.GetMoney();
        GameManager.instance.gameData.levelData[levelIndex].kitchenCount = kitchens.Length;
        GameManager.instance.gameData.levelData[levelIndex].goldEarnedPerSec = CalculateEarnedMoneyOfPerSeconds();
        GameManager.instance.gameData.levelData[levelIndex].lastLoginDate = DateTime.Now.ToString();
        GameManager.instance.gameData.levelData[levelIndex].parkinLotCount = parkinLot.Count;
        GameManager.instance.gameData.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;


        GameManager.instance.gameData.levelData[levelIndex].task1 = !bulasikhaneTask.activeInHierarchy;
        GameManager.instance.gameData.levelData[levelIndex].task2 = !mutfakTask.activeInHierarchy;
        GameManager.instance.gameData.levelData[levelIndex].task3 = !restoranTask.activeInHierarchy;
        for (int i = 0; i < GameManager.instance.gameData.levelData[levelIndex].kitchenCount; i++)
        {
            if(GameManager.instance.gameData.levelData[levelIndex].kitchenIsLocked == null) 
                continue;
            GameManager.instance.gameData.levelData[levelIndex].kitchenIsLocked[i] =  kitchens[i].isLocked;
            GameManager.instance.gameData.levelData[levelIndex].chefCount[i] =  kitchens[i].cookCount;
            GameManager.instance.gameData.levelData[levelIndex].counterCount[i] =  kitchens[i].counterCount;
            GameManager.instance.gameData.levelData[levelIndex].ovenCount[i] =  kitchens[i].ovenCount;
            GameManager.instance.gameData.levelData[levelIndex].pizzaCounterCount[i] =  kitchens[i].pizzaCounterCount;
            GameManager.instance.gameData.levelData[levelIndex].pizzaCounterCost[i] =  kitchens[i].pizzaCounterCost.GetGold();
            GameManager.instance.gameData.levelData[levelIndex].ovenCost[i] =  kitchens[i].ovenCost.GetGold();
            GameManager.instance.gameData.levelData[levelIndex].counterCost[i] =  kitchens[i].counterCost.GetGold();
            GameManager.instance.gameData.levelData[levelIndex].chefCost[i] =  kitchens[i].asciCost.GetGold();
        }
        GameManager.instance.gameData.levelData[levelIndex].sculleryCount =  scullery.Count;
        GameManager.instance.gameData.levelData[levelIndex].waiterCount =  restaurant.allWaiters.Count;
        GameManager.instance.gameData.levelData[levelIndex].waiterSpeed =  restaurant.moveSpeed;
        GameManager.instance.gameData.levelData[levelIndex].tableCount =  restaurant.tableCount;
        GameManager.instance.gameData.levelData[levelIndex].restaurantIsLock =  restaurant.isLocked;
        for (int i = 0; i < GameManager.instance.gameData.levelData[levelIndex].sculleryCount; i++)
        {
            GameManager.instance.gameData.levelData[levelIndex].sculleryIsLocked[i] = scullery[i].isLocked;
            GameManager.instance.gameData.levelData[levelIndex].dishwasherCount[i] = scullery[i].dishwasherCount;
            GameManager.instance.gameData.levelData[levelIndex].dishCounterCount[i] = scullery[i].dishCounterCount;
            GameManager.instance.gameData.levelData[levelIndex].sinkCount[i] = scullery[i].sinkCount;
        }
        
        for (int i = 0; i < GameManager.instance.gameData.levelData[levelIndex].parkinLotCount; i++)
        {
            //GameManager.instance.gameData.levelData[levelIndex].parkingLotIsLocked = parkinLot[i].isLocked;
            GameManager.instance.gameData.levelData[levelIndex].motorcycleCount[i] = parkinLot[i].allMotorcycle.Count;
            GameManager.instance.gameData.levelData[levelIndex].motorcycleSpeed[i] = parkinLot[i].hiz;
        }
        Debug.Log("saved");
    }
    public void LoadLevel()
    {
        isUnlock = true;
        lastLoginDate =  GameManager.instance.gameData.levelData[levelIndex].lastLoginDate;
        restaurant.moveSpeed = GameManager.instance.gameData.levelData[levelIndex].waiterSpeed;
        bulasikhaneTask.SetActive(!GameManager.instance.gameData.levelData[levelIndex].task1);
        mutfakTask.SetActive(!GameManager.instance.gameData.levelData[levelIndex].task2);
        restoranTask.SetActive(!GameManager.instance.gameData.levelData[levelIndex].task3);
        for (int i = 0; i < GameManager.instance.gameData.levelData[levelIndex].kitchenCount; i++)
        {
            kitchens[i].isLocked = GameManager.instance.gameData.levelData[levelIndex].kitchenIsLocked[i];
            
            if(!kitchens[i].isLocked)
            {
                if(kitchens[i].kuryeMutfagimi)
                {
                    kitchens[i].parkinLot.@lock.gameObject.SetActive(false);
                    kitchens[i].parkinLot.isLocked = false;

                }
                kitchens[i].@lock.gameObject.SetActive(false);
            }
            // if(kitchens[i].isLocked)
            // {
            //     kitchens[i].counterSayisi = GameManager.instance.gameData.levelDatas[levelIndex].counterSayisi[i];
            //     kitchens[i].pizzaCounterSayisi = GameManager.instance.gameData.levelDatas[levelIndex].pizzaCounterSayisi[i];
            //     kitchens[i].firinSayisi = GameManager.instance.gameData.levelDatas[levelIndex].firinSayisi[i];
            //     kitchens[i].asciSayisi = GameManager.instance.gameData.levelDatas[levelIndex].asciSayisi[i];
            // }
            // else
            // {
                var temp3 = GameManager.instance.gameData.levelData[levelIndex].ovenCount[i];
                kitchens[i].ovenCount = 0;
                for (int j = 0; j < temp3; j++)
                {
                    kitchens[i].FirinSatinAl(false);
                }
                var temp2 = GameManager.instance.gameData.levelData[levelIndex].counterCount[i];
                kitchens[i].counterCount = 0;
                for (int j = 0; j < temp2; j++)
                {
                    kitchens[i].BuyCounter(false);
                }
                var temp1 = GameManager.instance.gameData.levelData[levelIndex].pizzaCounterCount[i];
                kitchens[i].pizzaCounterCount = 0;
                for (int j = 0; j < temp1; j++)
                {
                    kitchens[i].PizzaCounterSatinAl(false);
                }
                var temp = GameManager.instance.gameData.levelData[levelIndex].chefCount[i];
                kitchens[i].cookCount = 0;
                for (int j = 0; j < temp; j++)
                {
                    kitchens[i].AsciSatinAl(false);
                }
            // }
        }
        
        for (int i = 0; i < GameManager.instance.gameData.levelData[levelIndex].sculleryCount; i++)
        {   
            scullery[i].isLocked = GameManager.instance.gameData.levelData[levelIndex].sculleryIsLocked[i];
            var counterTemp = GameManager.instance.gameData.levelData[levelIndex].dishCounterCount[i];
            scullery[i].dishCounterCount = 0;
            for (int j = 0; j < counterTemp; j++)
            {
                scullery[i].BulasikCounterSatinAl(false);
            }
            var sinktemp = GameManager.instance.gameData.levelData[levelIndex].sinkCount[i];
            scullery[i].sinkCount = 0;
            for (int j = 0; j < sinktemp; j++)
            {
                scullery[i].SinkSatinAl(false);
            }
            var bulasikciTemp = GameManager.instance.gameData.levelData[levelIndex].dishwasherCount[i];
            for (int j = 0; j < bulasikciTemp; j++)
            {
                scullery[i].BulasikciSatinAl(false);
            }
            if(!scullery[i].isLocked )
                scullery[i].@lock.SetActive(false);
        }
        var tempchair =  GameManager.instance.gameData.levelData[levelIndex].tableCount;
        restaurant.tableCount = 0;
        for (int i = 0; i < tempchair; i++)
        {
            restaurant.BuyTable(false);
        }
        restaurant.isLocked =  GameManager.instance.gameData.levelData[levelIndex].restaurantIsLock;
        var garsontemp = GameManager.instance.gameData.levelData[levelIndex].waiterCount;
        restaurant.customerCount = 0;
        for (int i = 0; i < garsontemp; i++)
        {
            restaurant.BuyWaiter(false);
        }   
        if( !restaurant. isLocked)
        {
            restaurant. @lock.SetActive(false);
        }
        for (int i = 0; i <  GameManager.instance.gameData.levelData[levelIndex].parkinLotCount; i++)
        {
            //parkinLot[i].isLocked = GameManager.instance.gameData.levelData[levelIndex].parkingLotIsLocked;
            parkinLot[i].motorcycleCount = GameManager.instance.gameData.levelData[levelIndex].motorcycleCount[i];
            parkinLot[i].hiz = GameManager.instance.gameData.levelData[levelIndex].motorcycleSpeed[i];
            var motorTemp = GameManager.instance.gameData.levelData[levelIndex].motorcycleCount[i];
            for (int j = 0; j < motorTemp; j++)
            {
                parkinLot[i].MotorcycleAl(false);
            }
            if(!parkinLot[i].isLocked)
            {
                parkinLot[i].@lock.SetActive(false);
                GameManager.instance.SetMoney(parkinLot[i].unlockCost.GetGold());
                // parkinLot[i].kitchen.UnLock();
            }
        }
    }
    GameManager gameManager;
    public void OpenMap()
    {
        Save();
        gameManager = FindObjectOfType<GameManager>();
        GameManager.LoadScene(1);

    }
    public float CalculateEarnedMoneyOfPerSeconds()
    {
        List<float> sort = new List<float>();
        var kitchenTotal = 0f;
        if (kitchens.Length == 0)
        {
            Debug.Log("return 0");
            return 0;
        }
        var kitchenCount = 0;
        for (int i = 0; i < kitchens.Length; i++)
        {
            //Debug.Log(kitchens[i] + " " + kitchens[i].PizzaMakingTime(), kitchens[i]);
            if(kitchens[i].PizzaMakingTime() == 0)
                continue;
            kitchenTotal += kitchens[i].PizzaMakingTime();
            kitchenCount ++;
        }
        if (kitchenTotal == 0)
            return 0;
        kitchenTotal = (kitchenTotal / kitchenCount) / kitchenCount;
        var sculleryTotal = 0f;
        for (int i = 0; i < scullery.Count; i++)
        {
            sculleryTotal += scullery[i].PizzaMakingTime();
        }        
        if (sculleryTotal == 0)
        {
            Debug.Log("return 0");
            return 0;
        }
        sculleryTotal = (sculleryTotal/scullery.Count) /scullery.Count;
        if (restaurant.isLocked == false)
        {
            Debug.Log("return 0");
            return 0;
        }
        sort.Add(kitchenTotal);
        sort.Add(restaurant.PizzaDistributingTime());
        sort.Add(sculleryTotal);
        sort.Sort();
        return restaurant.earnedMoneyFromCustomer / sort[0];
    }
    public void IdleMoneyCanvasActive()
    {
        idleMoneyCanvas.SetActive(false);
        StartCoroutine(GoldAnim.instance.EarnGoldAnim((int)(CalcPassingTime()*(CalculateEarnedMoneyOfPerSeconds())/10),20,GameManager.instance.idleMoneyText.transform));
    }
    public int CalcPassingTime()
    {
        string dateOld = lastLoginDate;
        if(string.IsNullOrEmpty(dateOld))
        {
            Debug.Log("firstgame");
        }
        else
        {
            // _Time time = NewTime.GetTime();
            DateTime _dateNow = Convert.ToDateTime(DateTime.Now);
            DateTime _dateOld = Convert.ToDateTime(lastLoginDate);
            TimeSpan diff = _dateNow.Subtract(_dateOld);
            return (int) diff.TotalSeconds;
        }
        
        return 0;
    }
    public bool IsRestaurantReady(bool isStart)
    {
        if(restaurant.isLocked)
            return false;
        var kitchenCount = 0;
        for (int i = 0; i < kitchens.Length; i++)
        {
            if(!kitchens[i].isLocked && !kitchens[i].kuryeMutfagimi)
            {
                kitchenCount++;
            }
        }
        if(kitchenCount == 0)
            return false;
        var sculleryCount =0;
        for (int i = 0; i < scullery.Count; i++)
        {
            if(!scullery[i].isLocked)
                sculleryCount ++;
        }
        if(sculleryCount == 0)
            return false;
        if(isStart)
        {
            StartCoroutine (CustomerCreator.instance.CustomerCreate());
            return true;
        }
        if(sculleryCount >= 1 && kitchenCount >= 1 && !restaurant.isLocked)
        {
            Debug.Log("true");
            StartCoroutine (CustomerCreator.instance.CustomerCreate());
            return true;
        }
        
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Kitchen : Department
{   
    public ParkinLot parkinLot;
    public KitchenUIData kitchenUIData; 
    public KitchenData kitchenData; 
    [Space(20)]
    public int kitchenIndex;
    [SerializeField] public bool kuryeMutfagimi;
    public Transform fridge;
    [HideInInspector] public int counterCount;
    [HideInInspector] public int pizzaCounterCount;
    [HideInInspector] public int ovenCount;
    [HideInInspector] public int cookCount;
    [SerializeField] GameObject _dataPanel;
    [SerializeField] Transform _camPlace;
    public override Level level {get; set;}
    public override GameObject dataPanel { get; set; }
    [SerializeField] public override Transform camPlace { get; set; }
    public Gold ovenCost;
    public Gold pizzaCounterCost;
    public Gold asciCost;
    public Gold counterCost;
    public int chefCapacity;
    [Space(10)]
    [Header("Listeler")]
    [SerializeField] private List<Oven> UseableOvens;
    [SerializeField] private List<Transform> chefWaitTransform;
    [SerializeField] public List<RollOutPizzaCounter> useablePizzaCounters;
    [SerializeField] public List<RollOutPizzaCounter> allPizzaCounters;
    public List<Counter> useableCounters;
    public List<Counter> allCounters;
    public List<Chef> allChefs;
    public List<Oven> allOven;
    void OnEnable()
    {
        levelManager = FindObjectOfType<LevelManager>();
        kitchenUIData = GetComponentInChildren<KitchenUIData>();
    }
   
    void Awake()
    {
        dataPanel = _dataPanel;
        camPlace = _camPlace;
        level = GetComponentInParent<Level>();
        selectableCollider = GetComponent<Collider>();
        allPizzaCounters = GetComponentsInChildren<RollOutPizzaCounter>().ToList();
        allOven = GetComponentsInChildren<Oven>().ToList();
        allCounters = GetComponentsInChildren<Counter>().ToList();
    }
    private void Start()
    {
        
    }
    
    public void SaveKitchen()
    {
        kitchenData = new KitchenData{
            kitchenIsLocked = isLocked,
            ovenCount = ovenCount,
            counterCount = counterCount,
            chefCount = cookCount,
            pizzaCounterCount = pizzaCounterCount,
            chefCost = asciCost.GetGold(),
            counterCost= counterCost.GetGold(),
            ovenCost = ovenCost.GetGold(),
            pizzaCounterCost = pizzaCounterCost.GetGold(),
        };
        if(parkinLot != null)
            parkinLot.SaveParkinglot();
        
        level.levelData.kitchenData[kitchenIndex] = kitchenData;
    }
    public void LoadKitchen()
    {
        if(level.levelData.kitchenData[kitchenIndex] != null)
        {
            kitchenData = level.levelData.kitchenData[kitchenIndex];
            isLocked = kitchenData.kitchenIsLocked;
            if(!isLocked)
            {    
                if(kuryeMutfagimi)
                {
                    lockedPanel = parkinLot.lockedPanel;
                    parkinLot.isLocked = false;
                    parkinLot.LoadParkingLot();
                    level.unlockedParkinLots.Add(parkinLot);
                }
                level.unlockedKitchens.Add(this);
                @lock.SetActive(false);
                for (int i = 0; i < kitchenData.ovenCount; i++)
                {
                    FirinSatinAl(false);
                }
                for (int i = 0; i < kitchenData.counterCount; i++)
                {
                    BuyCounter(false);
                }
                for (int i = 0; i < kitchenData.pizzaCounterCount; i++)
                {
                    PizzaCounterSatinAl(false);
                }
                for (int i = 0; i < kitchenData.chefCount; i++)
                {
                    AsciSatinAl(false);
                }
            }
        }
        else
        {
            kitchenData = new KitchenData();
        }
        
        asciCost.SetGold(kitchenData.chefCost);
        pizzaCounterCost.SetGold(kitchenData.pizzaCounterCost);
        ovenCost.SetGold(kitchenData.ovenCost);
        counterCost.SetGold(kitchenData.counterCost);
        kitchenUIData.UpdateData();
    }
    public RollOutPizzaCounter GetEmptyPizzaCounter()
    {
        var enAz = useablePizzaCounters[0];
        for (int i = 0; i < useablePizzaCounters.Count; i++)
        {
            if(useablePizzaCounters[i].chefs.Count < enAz.chefs.Count)
            {
                enAz = useablePizzaCounters[i];
            }
        }
        return enAz;
    }
    public Counter FindCounterWithMostCooks()
    {
        var enCok = useableCounters[0];
        for (int i = 0; i < useableCounters.Count; i++)
        {
            if(useableCounters[i].chefs.Count > enCok.chefs.Count)
            {
                enCok = useableCounters[i];
            }
        }
        return enCok;
    }
    public Oven FindOvenWithMostCook()
    {
        var enCok = UseableOvens[0];
        for (int i = 0; i < UseableOvens.Count; i++)
        {
            if(UseableOvens[i].chefs.Count > enCok.chefs.Count)
            {
                enCok = UseableOvens[i];
            }
        }
        return enCok;
    }
    public RollOutPizzaCounter FindPizzaCounterWithMostCook()
    {
        var enCok = useablePizzaCounters[0];
        for (int i = 0; i < useablePizzaCounters.Count; i++)
        {
                if(useablePizzaCounters[i].chefs.Count > enCok.chefs.Count)
            {
                enCok = useablePizzaCounters[i];
            }
        }
        return enCok;
    }
    public Counter GetEmptyCounter()
    {
        var enAz = useableCounters[0];
        for (int i = 0; i < useableCounters.Count; i++)
        {
            if(useableCounters[i].chefs.Count < enAz.chefs.Count)
            {
                enAz = useableCounters[i];
            }
        }
        return enAz;
    }
    public Oven GetEmptyOven()
    {
        var enAz = UseableOvens[0];
        for (int i = 0; i < UseableOvens.Count; i++)
        {
            if(UseableOvens[i].chefs.Count < enAz.chefs.Count)
            {
                enAz = UseableOvens[i];
            }
        }
        return enAz;
    }
    public void AsciSatinAl(bool isPain)
    {
        if(cookCount == chefCapacity)
            return;
        if(isPain)
        {
            if(GameManager.instance.GetMoney() < asciCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetMoney(-asciCost.GetGold());
            }
        }
        cookCount++;
        var asci = Instantiate(levelManager.cookPrefab,chefWaitTransform[0].position,Quaternion.identity);
        var asciClass = asci.transform.GetChild(0).GetComponent<Chef>();
        if(kuryeMutfagimi)
        {
            
            asciClass.counterFullState.kuryeAscisimi = true;
        }
        allChefs.Add(asciClass);
        var counter = GetEmptyCounter();
        asciClass.counter = counter;
        counter.chefs.Add(asci);
        
        var pizzaCounter = GetEmptyPizzaCounter();
        asciClass.rollOutPizzaCounter = pizzaCounter;   
        pizzaCounter.chefs.Add(asci);
    
        var oven = GetEmptyOven();
        asciClass.oven = oven;
        oven.chefs.Add(asci);
        
        asciClass.fridge = fridge;
        asciClass.kitchen = this;
        asciClass.level = level;
        asciClass.InitializeChef();
        asciCost.IncreaseGold(100);
        CheckChefButton();
        kitchenUIData.UpdateData();
        
    }
    public void BuyCounter(bool isPaid)
    {   
        if(counterCount == allCounters.Count)
            return;
        if(isPaid)
        {
            if(GameManager.instance.GetMoney() < counterCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetMoney(-counterCost.GetGold());
            }
        }
        counterCount ++;
        var counter = allCounters[counterCount-1];
        useableCounters.Add(counter);

        counter.barrier.SetActive(false);
        var most = FindCounterWithMostCooks();
        if(most.chefs.Count == 0)
            return;
        most.chefs[most.chefs.Count-1].transform.GetChild(0).GetComponent<Chef>().counter = counter;
        counter.chefs.Add(most.chefs[most.chefs.Count-1]);
        var cook = most.chefs[most.chefs.Count-1].transform.GetChild(0).GetComponent<Chef>();
        if((cook.currState == cook.queueState || cook.currState == cook.queueWaitState) && cook.queueState.previousState == cook.putOnCounterState)
        {
            cook.currState = cook.putOnCounterState;
        }
        
        if(most.queue.Contains(most.chefs[most.chefs.Count-1].transform.GetChild(0).GetComponent<Chef>()))
            most.queue.Remove(most.chefs[most.chefs.Count-1].transform.GetChild(0).GetComponent<Chef>());    
        most.chefs.Remove(most.chefs[most.chefs.Count-1]);
        counterCost.IncreaseGold(100);
        CheckChefButton();
        kitchenUIData.UpdateData();
    }
    public void PizzaCounterSatinAl(bool isPaid)
    {
        if(pizzaCounterCount == allPizzaCounters.Count)
            return;
        if(isPaid)
        {
            if(GameManager.instance.GetMoney() < pizzaCounterCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetMoney(-pizzaCounterCost.GetGold());
            }
        }
        pizzaCounterCount ++;
        var pizzaAcmaCounter = allPizzaCounters[pizzaCounterCount-1];
        useablePizzaCounters.Add(pizzaAcmaCounter);
        var encok = FindPizzaCounterWithMostCook();
        if(encok.chefs.Count == 0)
            return;
        var asci = encok.chefs[encok.chefs.Count-1].transform.GetChild(0).GetComponent<Chef>();
        asci.rollOutPizzaCounter = pizzaAcmaCounter;
        pizzaAcmaCounter.chefs.Add(encok.chefs[encok.chefs.Count-1]);
        if((asci.currState == asci.queueState || asci.currState == asci.queueWaitState) && asci.queueState.previousState == asci.waitForOvenState)
        {
            asci.currState = asci.rollOutPizzaState;
        }
        if(encok.queue.Contains(asci))
            encok.queue.Remove(asci);    
        encok.chefs.Remove(encok.chefs[encok.chefs.Count-1]);
        pizzaCounterCost.IncreaseGold(100);
        CheckChefButton();
        kitchenUIData.UpdateData();
    }
    public void FirinSatinAl(bool ucretlimi)
    {
        if(ovenCount == allOven.Count)
            return;
        if(ucretlimi)
        {
            if(GameManager.instance.GetMoney() < ovenCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetMoney(-ovenCost.GetGold());
            }
        }
        ovenCount ++;
            var firin = allOven[ovenCount-1];
        UseableOvens.Add(firin);
        var encok = FindOvenWithMostCook();
        if(encok.chefs.Count == 0 || encok.chefs.Count == 1)
        {
            
        }
        else
        {
            var asci = encok.chefs[encok.chefs.Count-1].transform.GetChild(0).GetComponent<Chef>();
            if(encok.queue.Contains(asci))
            {
                encok.queue.Remove(asci);
            }
            asci.oven = firin;
            asci.currState = asci.putOunOvenState;
            firin.chefs.Add(encok.chefs[encok.chefs.Count-1]);
            encok.chefs.Remove(encok.chefs[encok.chefs.Count-1]);
        }
        // if((asci.currState == asci.queueState || asci.currState == asci.queueWaitState) && asci.queueState.previousState == asci.waitForOvenState)
        // {
        // }
        ovenCost.IncreaseGold(100);
        CheckChefButton();
        kitchenUIData.UpdateData();
    }
    public void UnLock(bool kuryeMutfagimi)
    {
        if(unlockCost.GetGold() <= GameManager.instance.GetMoney())
        {
            //if (level.mutfakTask.activeInHierarchy == true)
            //    level.mutfakTask.SetActive(false);
            GameManager.instance.SetMoney(-unlockCost.GetGold());
            lockedPanel.SetActive(false);
            isLocked = false;
            @lock.SetActive(false);
            FirinSatinAl(false);
            BuyCounter(false);
            PizzaCounterSatinAl(false);
            AsciSatinAl(false);
            kitchenUIData.UpdateData();
            level.RestaurantReady(false);
            level.unlockedKitchens.Add(this);
            if(kuryeMutfagimi)
                SelectManager.instance.BackButton();
                
        }
    }
    public float PizzaMakingTime()
    {
        if(allChefs.Count == 0)
            return 0;
        int pizzaCounterDiff = allChefs.Count - pizzaCounterCount;
        int firinDiff = allChefs.Count - ovenCount;

        float totalTime = 0;
        for (int i = 0; i < allChefs.Count; i++)
        {
            totalTime += Vector3.Distance(allChefs[i].counter.transform.position,fridge.transform.position);
            totalTime += Vector3.Distance(allChefs[i].fridge.position,allChefs[i].rollOutPizzaCounter.transform.position)/allChefs[i].moveSpeed;
            totalTime += allChefs[i].asciIdleState.rollOutPizzaTime;
            totalTime += Vector3.Distance (allChefs[i].rollOutPizzaCounter.transform.position,allChefs[i].oven.transform.position)/allChefs[i].moveSpeed;
            totalTime += allChefs[i].waitForOvenState.time;
            totalTime += Vector3.Distance (allChefs[i].oven.transform.position,allChefs[i].counter.transform.position)/allChefs[i].moveSpeed;
        }
        totalTime /= allChefs.Count;
        if(pizzaCounterDiff>0)
            totalTime += allChefs[0].asciIdleState.rollOutPizzaTime;
        if(firinDiff > 0)
            totalTime += allChefs[0].waitForOvenState.time;
        return totalTime/allChefs.Count;
    }
    private void CheckChefButton()
    {
        int queueCount = 4;
        if( allChefs.Count >= useableCounters.Count*queueCount)
        {
            kitchenUIData.ToggleChefButton(false);
        }
        else if(allChefs.Count >= useablePizzaCounters.Count*queueCount)
        {
            kitchenUIData.ToggleChefButton(false);
        }
        else if(allChefs.Count >= UseableOvens.Count*queueCount)
        {
            kitchenUIData.ToggleChefButton(false);
        }
        else
        {
            kitchenUIData.ToggleChefButton(true);
        }
    }
}

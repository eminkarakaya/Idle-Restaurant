using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : Department
{
    
    public ParkinLot parkinLot;
    public MutfakData kitchenData; 
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
    [SerializeField] private List<Ocak> UseableOvens;
    [SerializeField] private List<Transform> chefWaitTransform;
    public List<Counter> allCounters;
    [SerializeField] public List<PizzaAcmaCounter> useablePizzaCounters;
    [SerializeField] public List<PizzaAcmaCounter> allPizzaCounters;
    public List<Counter> useableCounters;
    public List<Asci> allChefs;
    public List<Ocak> allOven;
    void OnEnable()
    {
        levelManager = FindObjectOfType<LevelManager>();
        kitchenData = GetComponentInChildren<MutfakData>();
    }
   
    void Awake()
    {
        dataPanel = _dataPanel;
        camPlace = _camPlace;
        level = GetComponentInParent<Level>();
        selectableCollider = GetComponent<Collider>();
    }
    private void Start()
    {
        if(kuryeMutfagimi)
            lockedPanel = parkinLot.lockedPanel;
    }
    public PizzaAcmaCounter GetEmptyPizzaCounter()
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
    public Ocak FindOvenWithMostCook()
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
    public PizzaAcmaCounter FindPizzaCounterWithMostCook()
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
    public Ocak GetEmptyOven()
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
        var asciClass = asci.transform.GetChild(0).GetComponent<Asci>();
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
        kitchenData.UpdateData();
        
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
        most.chefs[most.chefs.Count-1].transform.GetChild(0).GetComponent<Asci>().counter = counter;
        counter.chefs.Add(most.chefs[most.chefs.Count-1]);
        var cook = most.chefs[most.chefs.Count-1].transform.GetChild(0).GetComponent<Asci>();
        Debug.Log((cook.currState == cook.queueState || cook.currState == cook.queueWaitState) + " " +  (cook.queueState.previousState == cook.putOnCounterState),cook);
        if((cook.currState == cook.queueState || cook.currState == cook.queueWaitState) && cook.queueState.previousState == cook.putOnCounterState)
        {
            cook.currState = cook.putOnCounterState;
        }
        
        if(most.queue.Contains(most.chefs[most.chefs.Count-1].transform.GetChild(0).GetComponent<Asci>()))
            most.queue.Remove(most.chefs[most.chefs.Count-1].transform.GetChild(0).GetComponent<Asci>());    
        most.chefs.Remove(most.chefs[most.chefs.Count-1]);
        kitchenData.UpdateData();
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
        var asci = encok.chefs[encok.chefs.Count-1].transform.GetChild(0).GetComponent<Asci>();
        asci.rollOutPizzaCounter = pizzaAcmaCounter;
        pizzaAcmaCounter.chefs.Add(encok.chefs[encok.chefs.Count-1]);
        if((asci.currState == asci.queueState || asci.currState == asci.queueWaitState) && asci.queueState.previousState == asci.waitForOvenState)
        {
            asci.currState = asci.rollOutPizzaState;
        }
        if(encok.queue.Contains(asci))
            encok.queue.Remove(asci);    
        encok.chefs.Remove(encok.chefs[encok.chefs.Count-1]);
        kitchenData.UpdateData();
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
        if(encok.chefs.Count == 0)
        {
            return;
        }
        encok.chefs[encok.chefs.Count-1].transform.GetChild(0).GetComponent<Asci>().oven = firin;
        firin.chefs.Add(encok.chefs[encok.chefs.Count-1]);
        var asci = encok.chefs[encok.chefs.Count-1].transform.GetChild(0).GetComponent<Asci>();
        if((asci.currState == asci.queueState || asci.currState == asci.queueWaitState) && asci.queueState.previousState == asci.waitForOvenState)
        {
            asci.currState = asci.putOunOvenState;
        }
        if(encok.queue.Contains(asci))
            encok.queue.Remove(asci);
        encok.chefs.Remove(encok.chefs[encok.chefs.Count-1]);
        kitchenData.UpdateData();
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
            kitchenData.UpdateData();
            level.IsRestaurantReady(false);
            if(kuryeMutfagimi)
                SelectManager.instance.GeriButonu();
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
}

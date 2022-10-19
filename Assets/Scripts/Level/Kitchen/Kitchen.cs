using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : Department
{
    public int mutfakIndex;
    [SerializeField] bool kuryeMutfagimi;
    [Header("Listeler")]
    public List<Transform> asciBeklemeTransform;
    public List<Counter> allCounters;
    // public List<GameObject> buzdolablari;
    public List<PizzaAcmaCounter> kullanilanPizzaCounters;
    public List<PizzaAcmaCounter> allPizzaCounters;
    public List<Counter> kullanilanCounters;
    public List<Ocak> allFirin;
    public List<Ocak> kullanilanFirinlar;
    public List<Transform> buzdolabiYerleri;
    [Space(10)]
    public Transform buzdolabi;
    public int counterSayisi;
    public int pizzaCounterSayisi;
    public int firinSayisi;
    public int asciSayisi;
    [SerializeField] Gold unlockCost;
    [SerializeField] GameObject _acilacakPanel;
    [SerializeField] Transform _camPlace;
    public override Level level {get; set;}
    public override GameObject acilacakPanel { get; set; }
    [SerializeField] public override Transform camPlace { get; set; }
    public Gold firinCost;
    public Gold pizzaCounterCost;
    public Gold asciCost;
    public Gold counterCost;
    public int asciKapasite;


    void OnEnable()
    {
        levelManager = FindObjectOfType<LevelManager>();
        // LoadKitchen();
    }
   
    void Start()
    {
        if(!isLocked)
            _lock.SetActive(false);
        acilacakPanel = _acilacakPanel;
        camPlace = _camPlace;
        level = GetComponentInParent<Level>();
        selectableCollider = GetComponent<Collider>();
    }
    // public void LoadKitchen()
    // {
    //     KitchenDataSave data = SaveSystem.LoadKitchen();
    //     Debug.Log(this);
    //     asciSayisi = data.asciSayisi;
    //     Debug.Log(this.asciSayisi);
    //     counterSayisi = data.counterSayisi;
    //     pizzaCounterSayisi = data.pizzaCounterSayisi;
    //     firinSayisi = data.firinSayisi;
    //     isLocked = data.isLocked;
    //     for (int i = 0; i < asciSayisi; i++)
    //     {
    //         AsciSatinAl(false);
    //     }
    // }
    
    public PizzaAcmaCounter GetEmptyPizzaAcmaCounter()
    {
        var enAz = kullanilanPizzaCounters[0];
        for (int i = 0; i < kullanilanPizzaCounters.Count; i++)
        {
            if(kullanilanPizzaCounters[i].ascilar.Count < enAz.ascilar.Count)
            {
                enAz = kullanilanPizzaCounters[i];
            }
        }
        return enAz;
    }
    public Counter EnCokAsciliCounteriBul()
    {
        var enCok = kullanilanCounters[0];
        for (int i = 0; i < kullanilanCounters.Count; i++)
        {
            if(kullanilanCounters[i].ascilar.Count > enCok.ascilar.Count)
            {
                enCok = kullanilanCounters[i];
            }
        }
        return enCok;
    }
    public Ocak EnCokAsciliFirinBul()
    {
        var enCok = kullanilanFirinlar[0];
        for (int i = 0; i < kullanilanFirinlar.Count; i++)
        {
            if(kullanilanFirinlar[i].ascilar.Count > enCok.ascilar.Count)
            {
                enCok = kullanilanFirinlar[i];
            }
        }
        return enCok;
    }
    public PizzaAcmaCounter EnCokAsciliPizzaCounterBul()
    {
        var enCok = kullanilanPizzaCounters[0];
        for (int i = 0; i < kullanilanPizzaCounters.Count; i++)
        {
            if(kullanilanPizzaCounters[i].ascilar.Count > enCok.ascilar.Count)
            {
                enCok = kullanilanPizzaCounters[i];
            }
        }
        return enCok;
    }
    public Counter GetEmptyCounter()
    {
        var enAz = kullanilanCounters[0];
        for (int i = 0; i < kullanilanCounters.Count; i++)
        {
            if(kullanilanCounters[i].ascilar.Count < enAz.ascilar.Count)
            {
                enAz = kullanilanCounters[i];
            }
        }
        return enAz;
    }
    public Ocak GetEmptyFirin()
    {
        var enAz = kullanilanFirinlar[0];
        for (int i = 0; i < kullanilanFirinlar.Count; i++)
        {
            if(kullanilanFirinlar[i].ascilar.Count < enAz.ascilar.Count)
            {
                enAz = kullanilanFirinlar[i];
            }
        }
        return enAz;
    }
    public void AsciSatinAl(bool ucretlimi)
    {
        if(asciSayisi == asciKapasite)
            return;
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara() < asciCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetPara(-asciCost.GetGold());
            }
        }
        asciSayisi++;
        Debug.Log(levelManager.asciPrefab);
        Debug.Log(asciBeklemeTransform[0]);
        var asci = Instantiate(levelManager.asciPrefab,asciBeklemeTransform[0].position,Quaternion.identity);
        var asciClass = asci.transform.GetChild(0).GetComponent<Asci>();
        if(kuryeMutfagimi)
        {
            asciClass.counterFullState.kuryeAscisimi = true;
        }
        var counter = GetEmptyCounter();
        asciClass.counter = counter;
        counter.ascilar.Add(asci);
    
        var pizzaCounter = GetEmptyPizzaAcmaCounter();
        asciClass.pizzaAcmaCounter = pizzaCounter;   
        pizzaCounter.ascilar.Add(asci);
    
        var ocak = GetEmptyFirin();
        asciClass.ocak = ocak;
        ocak.ascilar.Add(asci);
        
        
        asciClass.buzdolabi = buzdolabi;
        asciClass.level = level;
        asciClass.kitchen = this;
    }
    public void KasaSatinAl(bool ucretlimi)
    {   
        if(counterSayisi == allCounters.Count)
            return;
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara() < counterCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetPara(-counterCost.GetGold());
            }
        }
        counterSayisi ++;
        var counter = allCounters[counterSayisi-1];
        kullanilanCounters.Add(counter);
        counter.engel.SetActive(false);
        var encok = EnCokAsciliCounteriBul();
        encok.ascilar[encok.ascilar.Count-1].transform.GetChild(0).GetComponent<Asci>().counter = counter;
        counter.ascilar.Add(encok.ascilar[encok.ascilar.Count-1]);
        var asci = encok.ascilar[encok.ascilar.Count-1].transform.GetChild(0).GetComponent<Asci>();
        if(asci.currState == asci.queueState || asci.currState == asci.queueBekleState || asci.currState == asci.firiniBeklemeState)
        {
            asci.currState = asci.countereKoymaState;
        }
        
        if(encok.queue.Contains(encok.ascilar[encok.ascilar.Count-1].transform.GetChild(0).GetComponent<Asci>()))
            encok.queue.Remove(encok.ascilar[encok.ascilar.Count-1].transform.GetChild(0).GetComponent<Asci>());    
        encok.ascilar.Remove(encok.ascilar[encok.ascilar.Count-1]);
        // while(EnCokAsciliCounteriBul().ascilar.Count >= counter.ascilar.Count)
        // {
        // }

    }
    public void PizzaCounterSatinAl(bool ucretlimi)
    {
        if(pizzaCounterSayisi == allPizzaCounters.Count)
            return;
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara() < pizzaCounterCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetPara(-pizzaCounterCost.GetGold());
            }
        }
        pizzaCounterSayisi ++;
        var pizzaAcmaCounter = allPizzaCounters[pizzaCounterSayisi-1];
        kullanilanPizzaCounters.Add(pizzaAcmaCounter);
        var encok = EnCokAsciliPizzaCounterBul();
        var asci = encok.ascilar[encok.ascilar.Count-1].transform.GetChild(0).GetComponent<Asci>();
        asci.pizzaAcmaCounter = pizzaAcmaCounter;
        pizzaAcmaCounter.ascilar.Add(encok.ascilar[encok.ascilar.Count-1]);
        if(asci.currState == asci.queueState || asci.currState == asci.queueBekleState || asci.currState == asci.firiniBeklemeState)
        {
            asci.currState = asci.pizzaAcmaState;
        }
        if(encok.queue.Contains(asci))
            encok.queue.Remove(asci);    
        encok.ascilar.Remove(encok.ascilar[encok.ascilar.Count-1]);
    }
    public void FirinSatinAl(bool ucretlimi)
    {
        if(firinSayisi == allFirin.Count)
            return;
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara() < firinCost.GetGold())
                return;    
            else
            {
                GameManager.instance.SetPara(-firinCost.GetGold());
            }
        }
        firinSayisi ++;
            var firin = allFirin[firinSayisi-1];
        kullanilanFirinlar.Add(firin);
        var encok = EnCokAsciliFirinBul();
        
        encok.ascilar[encok.ascilar.Count-1].transform.GetChild(0).GetComponent<Asci>().ocak = firin;
        firin.ascilar.Add(encok.ascilar[encok.ascilar.Count-1]);
        var asci = encok.ascilar[encok.ascilar.Count-1].transform.GetChild(0).GetComponent<Asci>();
        if(asci.currState == asci.queueState || asci.currState == asci.queueBekleState || asci.currState == asci.firiniBeklemeState)
        {
            asci.currState = asci.firinaKoymaState;
        }
        if(encok.queue.Contains(asci))
            encok.queue.Remove(asci);
        encok.ascilar.Remove(encok.ascilar[encok.ascilar.Count-1]);
    }
    public void UnLock()
    {
        if(unlockCost.GetGold() <= GameManager.instance.GetPara())
        {
            level.kitchens.Add(this);
            isLocked = false;
            _lock.SetActive(false);
            AsciSatinAl(false);
        }
    }
}

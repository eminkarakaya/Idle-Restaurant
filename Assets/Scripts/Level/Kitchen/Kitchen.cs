using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : Department
{
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
    [SerializeField] GameObject acilacakItemler;
    [SerializeField] GameObject _lock;
    public Transform buzdolabi;
    public int counterSayisi;
    public int pizzaCounterSayisi;
    public int asciKapasitesi;
    public int firinSayisi;
    public int asciSayisi;
    [SerializeField] Gold unlockCost;
    [SerializeField] GameObject _acilacakPanel;
    [SerializeField] Transform _camPlace;
    public override Level level {get; set;}
    public override GameObject acilacakPanel { get; set; }
    [SerializeField] public override Transform camPlace { get; set; }
    public override Collider selectableCollider { get; set; }
    public override Transform oldCamPlace { get; set; }

    void Start()
    {
        if(!isLocked)
            _lock.SetActive(false);
        acilacakPanel = _acilacakPanel;
        camPlace = _camPlace;
        level = GetComponentInParent<Level>();
        selectableCollider = GetComponent<Collider>();
    }
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
    public void AsciSatinAl()
    {
        var asci = Instantiate(GameManager.instance.asciPrefab,asciBeklemeTransform[asciSayisi].position,Quaternion.identity);
        
        var counter = GetEmptyCounter();
        asci.transform.GetChild(0).GetComponent<Asci>().counter = counter;
        counter.ascilar.Add(asci);
    
        var pizzaCounter = GetEmptyPizzaAcmaCounter();
        asci.transform.GetChild(0).GetComponent<Asci>().pizzaAcmaCounter = pizzaCounter;   
        pizzaCounter.ascilar.Add(asci);
    
        var ocak = GetEmptyFirin();
        asci.transform.GetChild(0).GetComponent<Asci>().ocak = ocak;
        ocak.ascilar.Add(asci);
        
        
        asci.transform.GetChild(0).GetComponent<Asci>().buzdolabi = buzdolabi;
        asci.transform.GetChild(0).GetComponent<Asci>().level = level;
        asci.transform.GetChild(0).GetComponent<Asci>().kitchen = this;
    }
    public void KasaSatinAl()
    {   
        counterSayisi ++;
        var counter = allCounters[counterSayisi-1];
        kullanilanCounters.Add(counter);
        counter.engel.SetActive(false);
        var encok = EnCokAsciliCounteriBul();
        encok.ascilar[encok.ascilar.Count-1].transform.GetChild(0).GetComponent<Asci>().counter = counter;
        counter.ascilar.Add(encok.ascilar[encok.ascilar.Count-1]);

        encok.ascilar.Remove(encok.ascilar[encok.ascilar.Count-1]);

        // while(EnCokAsciliCounteriBul().ascilar.Count >= counter.ascilar.Count)
        // {
        // }

    }
    public void PizzaCounterSatinAl()
    {
        pizzaCounterSayisi ++;
        var pizzaAcmaCounter = allPizzaCounters[pizzaCounterSayisi-1];
        kullanilanPizzaCounters.Add(pizzaAcmaCounter);
        var encok = EnCokAsciliPizzaCounterBul();
        encok.ascilar[encok.ascilar.Count-1].transform.GetChild(0).GetComponent<Asci>().pizzaAcmaCounter = pizzaAcmaCounter;
        pizzaAcmaCounter.ascilar.Add(encok.ascilar[encok.ascilar.Count-1]);
        encok.ascilar.Remove(encok.ascilar[encok.ascilar.Count-1]);
    }
    public void FirinSatinAl()
    {
        firinSayisi ++;
        var firin = allFirin[firinSayisi-1];
        kullanilanFirinlar.Add(firin);
        var encok = EnCokAsciliFirinBul();
        encok.ascilar[encok.ascilar.Count-1].transform.GetChild(0).GetComponent<Asci>().ocak = firin;
        firin.ascilar.Add(encok.ascilar[encok.ascilar.Count-1]);
        encok.ascilar.Remove(encok.ascilar[encok.ascilar.Count-1]);
    }
    public void UnLock()
    {
        if(unlockCost.GetGold() <= GameManager.instance.GetPara())
        {
            Debug.Log("unlocked");
            isLocked = false;
            _lock.SetActive(false);
            AsciSatinAl();
        }
    }
}

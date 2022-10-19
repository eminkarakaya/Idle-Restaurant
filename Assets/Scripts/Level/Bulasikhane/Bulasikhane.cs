using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulasikhane : Department
{
    BulasikhaneData bulasikhaneData;
    public override Level level {get; set;}
    public override GameObject acilacakPanel { get; set; }
    public override Transform camPlace { get; set; }
    [SerializeField] GameObject _acilacakPanel;
    [SerializeField] Transform _camPlace;
    public List<BulasikCounter> allBulasikCounter;
    public List<BulasikCounter> kullanilanCounters;
    public List<Sink> allSinks;
    public List<Sink> kullanilanSinks;
    public List<Bulasikci> allBulasikci;
    public int bulasikciKapasitesi = 3;
    public Gold bulasikCounterCost;
    public Gold sinkCost;
    public Gold bulasikciCost;
    public Gold unlockCost;
    public Transform bulasikciSpawn;
    public int sinkSayisi;
    public int bulasikciSayisi;
    public int bulasikCounterSayisi;
    void Awake()
    {
        level = GetComponentInParent<Level>();
        selectableCollider = GetComponent<Collider>();
        acilacakPanel = _acilacakPanel;
        camPlace = _camPlace;
        levelManager = FindObjectOfType<LevelManager>();
        bulasikhaneData = GetComponentInChildren<BulasikhaneData>();
    }
    public BulasikCounter FindEmptyBulasikCounter()
    {
        for (int i = 0; i < kullanilanCounters.Count; i++)
        {
            if(kullanilanCounters[i].tabaklar.Count == 0)
                return kullanilanCounters[i];
        }
        return kullanilanCounters[0];
    }
    public BulasikCounter FindBulasikCounter()
    {
        var enaz = kullanilanCounters[0];
        for (int i = 0; i < kullanilanCounters.Count; i++)
        {
            if(kullanilanCounters[i].bulasikcilar .Count == 0)
            {
                continue;
            }
            if(kullanilanCounters[i].tabaklar.Count < enaz.tabaklar.Count)
            {
                enaz = kullanilanCounters[i];
            }
        }
        return enaz;
    }
    public BulasikCounter EnCokBulasikciliCounteriBul()
    {
        var enCok = kullanilanCounters[0];
        for (int i = 0; i < kullanilanCounters.Count; i++)
        {
            if(kullanilanCounters[i].bulasikcilar.Count > enCok.bulasikcilar.Count)
            {
                enCok = kullanilanCounters[i];
            }
        }
        return enCok;
    }
    public Sink EnCokBulasikciliSinkiBul()
    {
        var enCok = kullanilanSinks[0];
        for (int i = 0; i < kullanilanSinks.Count; i++)
        {
            if(kullanilanSinks[i].bulasikcilar.Count > enCok.bulasikcilar.Count)
            {
                enCok = kullanilanSinks[i];
            }
        }
        return enCok;
    }
    
    public BulasikCounter GetEmptyBulasikCounter()
    {
        var enAz = kullanilanCounters[0];
        for (int i = 0; i < kullanilanCounters.Count; i++)
        {
            if(kullanilanCounters[i].bulasikcilar.Count < enAz.bulasikcilar.Count)
            {
                enAz = kullanilanCounters[i];
            }
        }
        return enAz;
    }
    public Sink GetEmptySink()
    {
        var enAz = kullanilanSinks[0];
        for (int i = 0; i < kullanilanSinks.Count; i++)
        {
            if(kullanilanSinks[i].bulasikcilar.Count < enAz.bulasikcilar.Count)
            {
                enAz = kullanilanSinks[i];
            }
        }
        return enAz;
    }
    public void BulasikciSatinAl(bool ucretlimi)
    {
        if(allBulasikci.Count > bulasikciKapasitesi)
        {
            return;
        }
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara()< bulasikciCost.GetGold())
                return;
            else
            {
                GameManager.instance.SetPara(-bulasikciCost.GetGold());
            }
        }
        bulasikciSayisi ++;
        var bulasikci = Instantiate(levelManager.bulasikciPrefab,bulasikciSpawn.position,Quaternion.identity);
        var counter = GetEmptyBulasikCounter();
        bulasikci.transform.GetChild(0).GetComponent<Bulasikci>().bulasikhane = this;
        bulasikci.transform.GetChild(0).GetComponent<Bulasikci>().bulasikCounter = counter;
        var bulasikciClass = bulasikci.transform.GetChild(0).GetComponent<Bulasikci>();
        allBulasikci.Add(bulasikciClass);
        counter.bulasikcilar.Add(bulasikci);
    
        var sink = GetEmptySink();
        bulasikciClass.sink = sink;   
        sink.bulasikcilar.Add(bulasikci);

        bulasikciClass.level = level;
        bulasikciClass.bulasikhane = this;
        bulasikciCost.SetGold(100);
        bulasikhaneData.UpdateData();

    }
    public void BulasikCounterSatinAl(bool ucretlimi)
    {
        if(kullanilanCounters.Count == allBulasikCounter.Count)
            return;
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara()< bulasikCounterCost.GetGold())
                return;
            else
            {
                GameManager.instance.SetPara(-bulasikCounterCost.GetGold());
            }
        }
        bulasikCounterSayisi++;
        var counter = allBulasikCounter[kullanilanCounters.Count];
        kullanilanCounters.Add(counter);
        counter.engel.SetActive(false);
        var encok = EnCokBulasikciliCounteriBul();
        encok.bulasikcilar[encok.bulasikcilar.Count-1].transform.GetChild(0).GetComponent<Bulasikci>().bulasikCounter = counter;
        counter.bulasikcilar.Add(encok.bulasikcilar[encok.bulasikcilar.Count-1]);
        var bulasikci = encok.bulasikcilar[encok.bulasikcilar.Count-1].transform.GetChild(0).GetComponent<Bulasikci>();
        if(bulasikci.currState == bulasikci.queueState || bulasikci.currState == bulasikci.queueBekleState || bulasikci.currState == bulasikci.bulasikTabakBekle)
        {
            if(encok.queue.Contains(bulasikci))
                encok.queue.Remove(bulasikci);
            bulasikci.currState = bulasikci.bulasikciTabakAl;
        }
        encok.bulasikcilar.Remove(encok.bulasikcilar[encok.bulasikcilar.Count-1]);
        bulasikCounterCost.SetGold(100);
        bulasikhaneData.UpdateData();
    }
    public void SinkSatinAl(bool ucretlimi)
    {
        if(kullanilanSinks.Count == allSinks.Count)
            return;
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara()< sinkCost.GetGold())
                return;
            else
            {
                GameManager.instance.SetPara(-sinkCost.GetGold());
            }
        }
        sinkSayisi++;
        var sink = allSinks[kullanilanSinks.Count];
        sink.gameObject.SetActive(true);
        kullanilanSinks.Add(sink);
        var encok = EnCokBulasikciliSinkiBul();
        encok.bulasikcilar[encok.bulasikcilar.Count-1].transform.GetChild(0).GetComponent<Bulasikci>().sink = sink;
        sink.bulasikcilar.Add(encok.bulasikcilar[encok.bulasikcilar.Count-1]);
        var bulasikci = encok.bulasikcilar[encok.bulasikcilar.Count-1].transform.GetChild(0).GetComponent<Bulasikci>();
        if(bulasikci.currState == bulasikci.queueState || bulasikci.currState == bulasikci.queueBekleState || bulasikci.currState == bulasikci.sinkBekleState)
        {
            bulasikci.currState = bulasikci.bulasikciTabakKoy;
        }
        encok.bulasikcilar.Remove(encok.bulasikcilar[encok.bulasikcilar.Count-1]);
        sinkCost.SetGold(100);
        bulasikhaneData.UpdateData();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : Department
{
    public List<Transform> asciBeklemeTransform;
    public int counterSayisi;
    public int pizzaCounterSayisi;
    public List<Counter> allCounters;
    public List<PizzaAcmaCounter> allPizzaCounters;
    public List<Counter> kullanimaAcikCounters;
    public int asciKapasitesi;
    public int firinSayisi;
    public List<Transform> firinYerleri;
    public List<Transform> buzdolabiYerleri;
    public int asciSayisi;
    public override Level Level {get; set;}
    [SerializeField] private Transform _camTransform;
    [SerializeField] private GameObject _acilacakPanel;
    public override GameObject acilacakPanel { get; set; }
    [SerializeField] public override Transform camPlace { get; set; }
    public override Collider selectableCollider { get; set; }
    public override Transform oldCamPlace { get; set; }

    void Start()
    {
        camPlace = _camTransform;
        selectableCollider = GetComponent<Collider>();
        acilacakPanel = _acilacakPanel;
    }
    public PizzaAcmaCounter GetEmptyPizzaAcmaCounter()
    {
        var enAz = allPizzaCounters[0];
        for (int i = 0; i < allPizzaCounters.Count; i++)
        {
            if(allPizzaCounters[i].ascilar.Count < enAz.ascilar.Count)
            {
                enAz = allPizzaCounters[i];
            }
        }
        return enAz;
    }
    public void AsciSatinAl()
    {
        var asci = Instantiate(GameManager.instance.asciPrefab,asciBeklemeTransform[asciSayisi].position,Quaternion.identity);
        for (int i = 0; i < allCounters.Count; i++)
        {
            if(allCounters[i].asci == null)
                asci.transform.GetChild(0).GetComponent<Asci>().counter = allCounters[i];
        }
        for (int i = 0; i < allPizzaCounters.Count; i++)
        {
            if(allPizzaCounters[i].ascilar == null)
            {
                asci.transform.GetChild(0).GetComponent<Asci>().pizzaAcmaCounter = GetEmptyPizzaAcmaCounter();
            }
        }
        
    }
    public void KasaSatinAl()
    {

    }
    public void pizzaCounterSatinAl()
    {

    }
    public void FirinSatinAl()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulasikhane : Department
{
    public override Level level {get; set;}
    public override GameObject acilacakPanel { get; set; }
    public override Transform camPlace { get; set; }
    public override Transform oldCamPlace { get; set; }
    public List<BulasikCounter> allCounter;
    public List<BulasikCounter> kullanilanCounters;
    public List<Sink> allSinks;
    public List<Sink> kullanilanSinks;
    void Awake()
    {
        level = GetComponentInParent<Level>();
        selectableCollider = GetComponent<Collider>();
    }
    public BulasikCounter FindBulasikCounter()
    {
        for (int i = 0; i < kullanilanCounters.Count; i++)
        {
            if(kullanilanCounters[i].tabaklar[kullanilanCounters[i].tabakSayisi-1] != null)
            {
                return kullanilanCounters[i];
            }
        }
        return null;
    }
    public void SinkSatinAl()
    {

    }
    public void BulasikciSatinAl()
    {
        
    }
}

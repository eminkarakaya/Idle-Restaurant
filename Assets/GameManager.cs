using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public List<int> acilanLeveller;
    public List<bool> isFull;
    public static GameManager instance {get;private set;}
    public List<Counter> allCounters;
    public List<Masa> allTables;
    public List<Chair> allChairs;
    public List<Chair> emptyChairs;
    public List<Musteri> allMusteris;
    public List<Musteri> yemekBekleyenMusteriler;
    public List<Chair> yemekBekleyenChairler;
    void Awake()
    {
        instance = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkinLot : Department
{
    public Item sira;
    ParkinlotData parkinlotData;
    public Transform spawnPoint;
    public override Level level {get; set;}
    [SerializeField] private GameObject _acilacakPanel;
    [SerializeField] private Transform _camPlace;
    public override GameObject acilacakPanel { get; set; }
    public override Transform camPlace { get; set; }
    public List<Counter> yemegiHazirCounterler;
    public int motorKapasitesi;
    public List<Motorcycle> tumMotorlar;    
    public Transform baslangicYeri;
    public Transform bitisYeri;
    public Gold motorCost;
    public Gold motorHiziCost;
    void Start()
    {
        selectableCollider = GetComponent<Collider>();
        acilacakPanel = _acilacakPanel;
        camPlace = _camPlace;
        parkinlotData = GetComponentInChildren<ParkinlotData>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    public void MotorcycleAl()
    {
        var motor = Instantiate(levelManager.motorPrefab,spawnPoint.position,Quaternion.identity);
        motor.GetComponent<Motorcycle>().level = level;
        motor.GetComponent<Motorcycle>().parkinLot = this;
        motor.GetComponent<Motorcycle>().sira = sira;
    }
}

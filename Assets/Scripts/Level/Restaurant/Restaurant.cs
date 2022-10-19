using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : Department 
{
    public List<Chair> kirliTabaklar;
    public List<Chair> yemekBekleyenChairler;
    public List<Counter> yemegiHazirCounterler;   
    public List<Chair> emptyChairs;
    public int garsonKapasitesi;
    RestorantData restorantData;
    public List<GameObject> tumGarsonlar;
    public List<GameObject> tumMasalar;
    public List<Transform> garsonBeklemeYerleri;
    public override Level level {get; set;}
    public override GameObject acilacakPanel { get; set; }
    public override Transform camPlace { get; set; }
    [SerializeField] private Transform _camTransform;
    [SerializeField] private GameObject _acilacakPanel;
    public Gold garsonUcreti;
    public Gold masaUcreti;
    public Gold garsonMoveUcreti;
    public Gold musteriSiklikUcreti;
    [HideInInspector] public float frekansAzalisYuzdesi = 3;
    [HideInInspector] public float hareketHiziArtisYuzdesi = 4;
    [HideInInspector] public float frekansNext;
    [HideInInspector] public float moveNext;
    [HideInInspector]public int masaSayisi = 1;
    [HideInInspector]public int masaKapasitesi = 11;
    [HideInInspector]public int garsonSayisi = 1;
    public float moveSpeed = 2;
    void OnEnable()
    {
        restorantData = GetComponentInChildren<RestorantData>();
        
    }
    void Start()
    {
        level = GetComponentInParent<Level>();
        levelManager = FindObjectOfType<LevelManager>();
        camPlace = _camTransform;
        selectableCollider = GetComponent<Collider>();
        acilacakPanel = _acilacakPanel;
    }
    public void GarsonSatinAl(bool ucretlimi)
    {
        if(garsonKapasitesi == garsonSayisi)
        {
            return;
        }
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara() < garsonUcreti.GetGold())
                return;    
            else
            {
                GameManager.instance.SetPara(-garsonUcreti.GetGold());
            }
        }
        GameManager.instance.SetPara(-garsonUcreti.GetGold());
        garsonSayisi ++;
        var garson = Instantiate(levelManager.garsonPrefab,garsonBeklemeYerleri[garsonSayisi].position,Quaternion.identity);
        garson.GetComponentInChildren<Garson>().beklemeYeri = garsonBeklemeYerleri[garsonSayisi];
        garson.GetComponentInChildren<Garson>().level = level;
        garson.GetComponentInChildren<Garson>().moveSpeed = moveSpeed;
        garsonUcreti.SetGold(100);
        tumGarsonlar.Add(garson);
        restorantData.UpdateData();
    }
    public void MasaSatinAl(bool ucretlimi)
    {
        if(masaSayisi == masaKapasitesi)
            return;
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara() < masaUcreti.GetGold())
                return;    
            else
            {
                GameManager.instance.SetPara(-masaUcreti.GetGold());
            }
        }
        masaSayisi ++;
        tumMasalar[masaSayisi-1].gameObject.SetActive(true);
        emptyChairs.Add(tumMasalar[masaSayisi-1].transform.GetChild(0).GetComponent<Chair>());
        emptyChairs.Add(tumMasalar[masaSayisi-1].transform.GetChild(2).GetComponent<Chair>());
        masaUcreti.SetGold(100);
        restorantData.UpdateData();
    }
    public void GarsonHareketHiziArttir(bool ucretlimi)
    {
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara() < garsonMoveUcreti.GetGold())
                return;    
            else
            {
                GameManager.instance.SetPara(-garsonMoveUcreti.GetGold());
            }
        }

        GameManager.instance.SetPara(-garsonMoveUcreti.GetGold());
        moveSpeed += moveSpeed * (hareketHiziArtisYuzdesi/100);
        for (int i = 0; i < tumGarsonlar.Count; i++)
        {
            tumGarsonlar[i].transform.GetChild(0).GetComponent<Garson>().moveSpeed = moveSpeed;
        }
        moveNext = tumGarsonlar[0].transform.GetChild(0).GetComponent<Garson>().moveSpeed + tumGarsonlar[0].transform.GetChild(0).GetComponent<Garson>().moveSpeed * (hareketHiziArtisYuzdesi/100);            
        garsonMoveUcreti.SetGold(100);
        restorantData.UpdateData();
    }
    public void MusteriSikligiArttir(bool ucretlimi)
    {
        if(ucretlimi)
        {
            if(GameManager.instance.GetPara() < musteriSiklikUcreti.GetGold())
                return;    
            else
            {
                GameManager.instance.SetPara(-musteriSiklikUcreti.GetGold());
            }
        }
        if(GameManager.instance.GetPara() < musteriSiklikUcreti.GetGold())
            return;
        GameManager.instance.SetPara(-musteriSiklikUcreti.GetGold());
        GetComponentInChildren<CustomerCreator>().frequency -= (GetComponentInChildren<CustomerCreator>().frequency * (frekansAzalisYuzdesi/100));
        frekansNext = GetComponentInChildren<CustomerCreator>().frequency - (GetComponentInChildren<CustomerCreator>().frequency * (frekansAzalisYuzdesi/100));
        musteriSiklikUcreti.SetGold(100);
        restorantData.UpdateData();
    }
}

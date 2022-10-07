using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : Department 
{
    public List<Chair> emptyChairs;
    public int garsonKapasitesi;
    Level level;
    RestorantData restorantData;
    public List<GameObject> tumGarsonlar;
    public List<GameObject> tumMasalar;
    public List<Transform> garsonBeklemeYerleri;
    public override Level Level {get; set;}
    public override GameObject acilacakPanel { get; set; }
    public override Transform camPlace { get; set; }
    public override Transform oldCamPlace { get; set; }
    public override Collider selectableCollider { get; set; }
    [SerializeField] private Transform _camTransform;
    [SerializeField] private GameObject _acilacakPanel;
    [HideInInspector]public int garsonUcreti = 100;
    [HideInInspector]public int masaUcreti = 100;
    [HideInInspector]public int garsonMoveUcreti = 100;
    [HideInInspector] public int musteriSiklikUcreti = 100;
    [HideInInspector] public float frekansAzalisYuzdesi = 3;
    [HideInInspector] public float hareketHiziArtisYuzdesi = 4;
    [HideInInspector] public float frekansNext;
    [HideInInspector] public float moveNext;
    [HideInInspector]public int masaSayisi = 1;
    [HideInInspector]public int garsonSayisi = 1;
    void Start()
    {
        level = GetComponentInParent<Level>();
        restorantData = GetComponentInChildren<RestorantData>();
        camPlace = _camTransform;
        selectableCollider = GetComponent<Collider>();
        acilacakPanel = _acilacakPanel;
    }
    public void GarsonSatinAl()
    {
        if(garsonKapasitesi == garsonSayisi)
        {
            return;
        }
        garsonSayisi ++;
        var garson = Instantiate(GameManager.instance.garsonPrefab,garsonBeklemeYerleri[garsonSayisi].position,Quaternion.identity);
        garson.GetComponentInChildren<Garson>().beklemeYeri = garsonBeklemeYerleri[garsonSayisi];
        garsonUcreti += 100;
        tumGarsonlar.Add(garson);
        restorantData.UpdateData();
    }
    public void MasaSatinAl()
    {
        masaSayisi ++;
        tumMasalar[masaSayisi].gameObject.SetActive(true);
        emptyChairs.Add(tumMasalar[masaSayisi].transform.GetChild(0).GetComponent<Chair>());
        emptyChairs.Add(tumMasalar[masaSayisi].transform.GetChild(2).GetComponent<Chair>());
        masaUcreti += 100;
        restorantData.UpdateData();
    }
    public void GarsonHareketHiziArttir()
    {
        for (int i = 0; i < tumGarsonlar.Count; i++)
        {
            tumGarsonlar[i].transform.GetChild(0).GetComponent<Garson>().moveSpeed += tumGarsonlar[i].transform.GetChild(0).GetComponent<Garson>().moveSpeed * (hareketHiziArtisYuzdesi/100);            
        }
        moveNext = tumGarsonlar[0].transform.GetChild(0).GetComponent<Garson>().moveSpeed + tumGarsonlar[0].transform.GetChild(0).GetComponent<Garson>().moveSpeed * (hareketHiziArtisYuzdesi/100);            
        garsonMoveUcreti+=100;
        restorantData.UpdateData();
    }
    public void MusteriSikligiArttir()
    {
        GetComponentInChildren<CustomerCreator>().frequency -= (GetComponentInChildren<CustomerCreator>().frequency * (frekansAzalisYuzdesi/100));
        frekansNext = GetComponentInChildren<CustomerCreator>().frequency - (GetComponentInChildren<CustomerCreator>().frequency * (frekansAzalisYuzdesi/100));
        musteriSiklikUcreti += 100;
        restorantData.UpdateData();
    }
}

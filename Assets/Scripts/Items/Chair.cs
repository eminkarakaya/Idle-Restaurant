using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Level level;
    [SerializeField] public Musteri musteri;
    public Transform oturulcakYer;
    public Transform tabakYeri;
    public Tabak tabak;
    public GameObject pizza;
    void Awake()
    {
        level = GetComponentInParent<Level>();
    }
    public void SetMusteri(Musteri musteri)
    {
        this.musteri = musteri;
    }
    public Musteri GetMusteri()
    {
        return musteri;
    }

    
    public void MasadanKalkma()
    {
        level.restaurant.kirliTabaklar.Add(this);
        Destroy(tabak.transform.GetChild(0).GetChild(1).gameObject);
        // Destroy(tabak.gameObject);
        // tabak.isDirty = true;
        musteri = null;
        
    }
    
}

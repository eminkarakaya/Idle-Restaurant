using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Level level;
    [SerializeField] private Musteri musteri;
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
        Destroy(pizza);
        Destroy(tabak.gameObject);
        // tabak.isDirty = true;
        musteri = null;
        
    }
    
}

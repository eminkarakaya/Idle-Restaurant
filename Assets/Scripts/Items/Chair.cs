using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Level level;
    [SerializeField] public Customer customer;
    public Transform placeToSit;
    public Transform platePlace;
    public Plate plate;
    public GameObject pizza;
    void Awake()
    {
        level = GetComponentInParent<Level>();
    }
    public void SetMusteri(Customer musteri)
    {
        this.customer = musteri;
    }
    public Customer GetMusteri()
    {
        return customer;
    }

    
    public void MasadanKalkma()
    {
        level.restaurant.dirtyPlates.Add(this);
        plate.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        ObjectPool.instance.pools[0].pooledObjects.Enqueue(plate.transform.GetChild(0).GetChild(1).gameObject);
        // Destroy(tabak.gameObject);
        // tabak.isDirty = true;
        customer = null;
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCreator : MonoBehaviour
{
    Level level;
    [SerializeField] GameObject musteriPrefab;
    [SerializeField] Transform customerCreatedTransform;
    public float frequency;
    public float frequencyTemp;
    void Awake()
    {
        level = GetComponentInParent<Level>();
    }
    void Start()
    {
        frequencyTemp = frequency;
        // var musteri = Instantiate(musteriPrefab,customerCreatedTransform.position,Quaternion.identity);
            // musteri.transform.GetChild(0).GetComponent<Musteri>().kapi = kapi;
            // musteri.transform.GetChild(0).GetComponent<Musteri>().level = level;
            // musteri.transform.SetParent(level.transform);
    }
    void Update()
    {
        CustomerCreate();
    }
    void CustomerCreate()
    {
        if(kapi.instance.queue.Count > kapi.instance.createdQueueTransform.Count-5)
            return;
        frequencyTemp -= Time.deltaTime;
        if(frequencyTemp < 0)
        {
            frequencyTemp = frequency;
            var musteri = Instantiate(musteriPrefab,customerCreatedTransform.position,Quaternion.identity);
            musteri.transform.GetChild(0).GetComponent<Musteri>().kapi = kapi.instance.kapiTransform;
            musteri.transform.GetChild(0).GetComponent<Musteri>().level = level;
            musteri.transform.SetParent(level.transform);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCreator : MonoBehaviour
{
    public static CustomerCreator instance;
    Level level;
    [SerializeField] GameObject customerPrefab;
    [SerializeField] Transform customerCreatedTransform;
    public float frequency;
    public float frequencyTemp;
    int queueCapacity = 5;
    void Awake()
    {
        instance = this;
        level = GetComponentInParent<Level>();
    }
    void Start()
    {
        frequencyTemp = frequency;
        // StartCoroutine (CustomerCreate());
    }
    
    public IEnumerator CustomerCreate()
    {
        while(true)
        {
            if(Door.instance.queue.Count < queueCapacity)
                frequencyTemp -= Time.deltaTime;
                
            if(frequencyTemp < 0)
            {
                frequencyTemp = frequency;
                var musteri = ObjectPool.instance.GetPooledObject(2);
                musteri.SetActive(true);
                musteri.transform.position = customerCreatedTransform.position;
                musteri.transform.rotation = Quaternion.identity;
                // var musteri = Instantiate(customerPrefab,customerCreatedTransform.position,Quaternion.identity);
                musteri.transform.GetChild(0).GetComponent<Customer>().door = Door.instance.kapiTransform;
                musteri.transform.GetChild(0).GetComponent<Customer>().currState = musteri.transform.GetChild(0).GetComponent<Customer>().customerWalkState;
                musteri.transform.GetChild(0).GetComponent<Customer>().level = level;
                musteri.transform.SetParent(level.transform);
                musteri.transform.GetChild(0).GetComponent<Customer>().currState = musteri.transform.GetChild(0).GetComponent<Customer>().customerWalkState;
            }
            yield return null;
        }
    }
}

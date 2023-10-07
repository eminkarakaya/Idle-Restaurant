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
                GameObject customerGameobject = ObjectPool.instance.GetPooledObject(2);
                Customer customer = customerGameobject.transform.GetChild(0).GetComponent<Customer>();
                customer.currState = customer.customerWalkState;
                customerGameobject.transform.position = customerCreatedTransform.position;
                customerGameobject.SetActive(true);
                customerGameobject.transform.rotation = Quaternion.identity;
                // var musteri = Instantiate(customerPrefab,customerCreatedTransform.position,Quaternion.identity);
                customer.door = Door.instance.doorTransform;
                // musteri.transform.GetChild(0).GetComponent<Customer>().currState = musteri.transform.GetChild(0).GetComponent<Customer>().customerWalkState;
                customer.level = level;
                customerGameobject.transform.SetParent(level.transform);
                // musteri.transform.GetChild(0).GetComponent<Customer>().currState = musteri.transform.GetChild(0).GetComponent<Customer>().customerWalkState;
            }
            yield return null;
        }
    }
}

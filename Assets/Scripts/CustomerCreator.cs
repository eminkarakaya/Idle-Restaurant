using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCreator : MonoBehaviour
{
    Level level;
    [SerializeField] Transform kapi;
    [SerializeField] GameObject musteriPrefab;
    [SerializeField] Transform customerCreatedTransform;
    public float frequency;
    void Awake()
    {
        level = GetComponentInParent<Level>();
    }
    void Start()
    {
        StartCoroutine(CustomerCreate());
        // var musteri = Instantiate(musteriPrefab,customerCreatedTransform.position,Quaternion.identity);
            // musteri.transform.GetChild(0).GetComponent<Musteri>().kapi = kapi;
            // musteri.transform.GetChild(0).GetComponent<Musteri>().level = level;
            // musteri.transform.SetParent(level.transform);
    }
    IEnumerator CustomerCreate()
    {
        while (true)
        {
            yield return new WaitForSeconds(frequency);
            var musteri = Instantiate(musteriPrefab,customerCreatedTransform.position,Quaternion.identity);
            musteri.transform.GetChild(0).GetComponent<Musteri>().kapi = kapi;
            musteri.transform.GetChild(0).GetComponent<Musteri>().level = level;
            musteri.transform.SetParent(level.transform);
        }
        
    }
}

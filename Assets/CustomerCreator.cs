using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCreator : MonoBehaviour
{
    [SerializeField] Transform kapi;
    [SerializeField] GameObject musteriPrefab;
    [SerializeField] Transform customerCreatedTransform;
    public float frequency;
    void Start()
    {
        StartCoroutine(CustomerCreate());
    }
    IEnumerator CustomerCreate()
    {
        yield return frequency;
        var musteri = Instantiate(musteriPrefab,customerCreatedTransform.position,Quaternion.identity);
        musteri.transform.GetChild(0).GetComponent<Musteri>().kapi = kapi;
    }
}

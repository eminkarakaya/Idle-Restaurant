using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculleryCounter : Item
{
    public Scullery scullery;
    public GameObject barrier;
    public List <Plate> plates;
    public Transform dishwasherPlace;
    public Item waiterItem;
    public bool CheckQueueCapacityIsFull()
    {
        return waiterItem.queue.Count >= waiterItem.createdQueueTransform.Count;
    }
    private void Start() {
        scullery = GetComponentInParent<Scullery>();
    }
}

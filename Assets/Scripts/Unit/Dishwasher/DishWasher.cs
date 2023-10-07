using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DishWasher : Unit
{
    public DishwasherWaitingForSinkState waitSinkState;
    public DishwasherWaitingForPlateState waitPlateState;
    public DishwasherTakingPlateState takePlateState;
    public DishwasherPlatePuttingState putPlateState;
    public DishwasherWashState washState;
    [Space(10)]
    public Scullery scullery;
    public Sink sink;
    public SculleryCounter dishCounter;
    void Awake()
    {  
        level = FindObjectOfType<Level>();
    }
    void Start()
    {
        action = GetComponent<Action>();
        agent = GetComponent<NavMeshAgent>();
        currState = takePlateState;
        currState.StartState(action);
    }
    void Update()
    {
        currState.UpdateState(action);
    }
    public void CheckCounter()
    {
        if(dishCounter.plates.Count != 0)
        {
            queueImage.gameObject.SetActive(false);
            dishCounter.plates[dishCounter.plates.Count-1].transform.SetParent(transform);
            dishCounter.plates[dishCounter.plates.Count-1].transform.position = hand.position;
            dishCounter.plates[dishCounter.plates.Count-1].transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
            washState.plate = dishCounter.plates[dishCounter.plates.Count-1];
            dishCounter.plates.RemoveAt(dishCounter.plates.Count-1);
            currState = putPlateState;
            dishCounter.UpdateQueue(this);
        }
    }
}

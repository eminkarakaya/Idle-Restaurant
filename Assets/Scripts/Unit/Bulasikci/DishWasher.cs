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
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chef : Unit
{
    public Kitchen kitchen;
    ParkinLot parkinLot;
    public ChefWaitingForOvenState waitForOvenState;
    public CounterFullState counterFullState;
    public ChefIdleState asciIdleState;
    public PuttingOnCounterState putOnCounterState;
    public ChefFridgeState fridgeState;
    public RollOutPizzaState rollOutPizzaState;
    public BakingState putOunOvenState;
    public GameObject pizzaPrefab;
    public GameObject pizza;
    public GameObject dough;
    public Oven oven;
    public RollOutPizzaCounter rollOutPizzaCounter;
    public Counter counter;
    public Transform fridge;
    [SerializeField] private float _moveSpeed;
    public float moveSpeed{
        get => _moveSpeed;
        set{
            _moveSpeed = value;
            agent.speed = _moveSpeed;
        }
    }
    void Start()
    {
        dough = level.dough;
        pizza = level.pizza;
        // Debug.Log(kitchen);        
        // fridge = kitchen.fridge;
        // level = FindObjectOfType<Level>();
        action = GetComponent<Action>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        currState = fridgeState;
        currState.StartState(action);
    }
    
    void Update()
    {
        currState.UpdateState(action);
    }
}

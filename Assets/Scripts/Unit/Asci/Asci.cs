using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Asci : Unit
{
    public Kitchen kitchen;
    ParkinLot parkinLot;
    public AsciFiriniBeklemeState waitForOvenState;
    public CounterFullState counterFullState;
    public AsciIdleState asciIdleState;
    public CountereKoymaState putOnCounterState;
    public BuzdolabiState fridgeState;
    public PizzaAcmaState rollOutPizzaState;
    public FirinaKoymaState putOunOvenState;
    public GameObject pizzaPrefab;
    public GameObject pizza;
    public GameObject dough;
    public Ocak oven;
    public PizzaAcmaCounter rollOutPizzaCounter;
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

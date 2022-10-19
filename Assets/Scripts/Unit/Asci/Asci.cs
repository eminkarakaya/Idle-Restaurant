using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Asci : Unit
{
    public Kitchen kitchen;
    public AsciFiriniBeklemeState firiniBeklemeState;
    public CounterFullState counterFullState;
    public AsciIdleState asciIdleState;
    public CountereKoymaState countereKoymaState;
    public BuzdolabiState buzdolabiState;
    public PizzaAcmaState pizzaAcmaState;
    public FirinaKoymaState firinaKoymaState;
    public GameObject pizzaPrefab;
    public GameObject pizza;
    public GameObject hamur;
    public Ocak ocak;
    public PizzaAcmaCounter pizzaAcmaCounter;
    public Counter counter;
    public Transform buzdolabi;
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
        action = GetComponent<Action>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        currState = buzdolabiState;
        currState.StartState(action);
        counter = kitchen.GetEmptyCounter();
        ocak = kitchen.GetEmptyFirin();
        pizzaAcmaCounter = kitchen.GetEmptyPizzaAcmaCounter();
    }
    
    void Update()
    {
        currState.UpdateState(action);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Asci : Unit
{
    public Action action;
    [SerializeField] private AsciState _currState;
    public AsciState currState
    {
        get => _currState;
        set{
            
            _currState = value;
            _currState.StartState(action);
        }
    }
    public Level level;
    public AsciFiriniBeklemeState firiniBeklemeState;
    public CounterFullState counterFullState;
    public AsciIdleState asciIdleState;
    public CountereKoymaState countereKoymaState;
    public BuzdolabiState buzdolabiState;
    public QueueState queueState;
    public PizzaAcmaState pizzaAcmaState;
    public FirinaKoymaState firinaKoymaState;
    public QueueBekleState queueBekleState;
    public GameObject pizza;
    public GameObject hamur;
    Vector3 rot = new Vector3(176.847f,120.684f,-48.271f);
    Vector3 offset = new Vector3(-0.05f,120.684f,-48.271f);
    public Transform hand;
    public GameObject pan;
    public Ocak ocak;
    public PizzaAcmaCounter pizzaAcmaCounter;
    public Counter counter;
    bool isCooking;
    public Transform ocakTransform;
    public Transform counterPos;
    public NavMeshAgent agent;
    public float cookingTime;
    public float cookingTimeTemp;
    public Transform buzdolabi;
    GameObject _hamur;
    GameObject _pizza;
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
        cookingTimeTemp = cookingTime;
        currState = buzdolabiState;
        currState.StartState(action);
    }
    void Update()
    {
        currState.UpdateState(action);
    }
}

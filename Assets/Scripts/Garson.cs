using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Garson : Unit
{
    public GarsonState _currentState;
    public GarsonState currentState{
        get{return _currentState; }
        set{
            _currentState = value;
            _currentState.StartState(action);
        }
    }
    public Transform beklemeYeri;
    public Level level;
    public WalkState walkState;
    public IdleState idleState;
    public TasiState tasiState;
    public YemekleBekleState yemekleBekleState;
    public Action action;
    public bool eliDolumu;
    Animator animator;
    public Transform hand;
    public NavMeshAgent agent;
    [SerializeField] public Tabak plate;
    public Tabak _plate;
    public GameObject _pizza;
    public Counter counter;
    [SerializeField] public Chair _chair;
    Transform garsonPos;
    Transform chairTabakyeri;
    [SerializeField] private float _moveSpeed;
    public float moveSpeed{
        get => _moveSpeed;
        set{
            _moveSpeed = value;
            agent.speed = _moveSpeed;
        }
    }
    void Awake()
    {
        // currentState.StartState(action);
        animator = GetComponent<Animator>();
        action = GetComponent<Action>();
        agent = GetComponent<NavMeshAgent>();
        // StartCoroutine(CountereGit());
    }
    // elınde pızza varken masa aramıyo
    void Start()
    {
        agent.speed = moveSpeed;
        currentState = idleState;
    }
    public Chair FindChair()
    {
        if(level.yemekBekleyenChairler.Count == 0)
        {
            return null;
        }
        var chair = level.yemekBekleyenChairler[0];
        chairTabakyeri = chair.tabakYeri;
        level.yemekBekleyenChairler.Remove(level.yemekBekleyenChairler[0]);
        this._chair = chair;
        return chair;
    }
   
    public Counter FindCounter()
    {
        if(level.yemegiHazirCounterler.Count == 0)
            return null;
        Counter nearestCounter = level.yemegiHazirCounterler[0];
        float nearestDistance = Vector3.Distance(level.yemegiHazirCounterler[0].transform.position,this.transform.position);
        for (int i = 0; i < level.yemegiHazirCounterler.Count; i++)
        {
            float distance = Vector3.Distance(level.yemegiHazirCounterler[i].transform.position,transform.position);
            if(distance < nearestDistance)
            {
                nearestCounter = level.yemegiHazirCounterler[i];
            }
        }
        var _counter = nearestCounter;
        garsonPos = _counter.garsonPos;
        level.yemegiHazirCounterler.Remove(nearestCounter);
        this.counter = _counter;
        return _counter;
    }
    void Update()
    {
         currentState.UpdateState(action);
    }

    void RunStateMachine()
    {
        // GarsonState state = currentState.UpdateState(action);
        // if(currentState != null)
        // {
        //     currentState = state;
        // }
    }
    // public IEnumerator CountereGit()
    // {
    //     if(FindCounter() == null)
    //     {
    //         agent.SetDestination(beklemeYeri.position);
    //         animator.SetBool("yuru",true);
    //         animator.SetBool("tasi",false);
    //     }   
    //     var counter = FindCounter();
    //     while(counter == null)
    //     {
    //         counter = FindCounter();
    //         Debug.Log(counter);
    //         yield return null;
    //     }
    //     agent.SetDestination(garsonPos.position);
    //     while(Vector3.Distance(agent.transform.position,garsonPos.position) > 1f)
    //     {
    //         Debug.Log("siparisi almaya gidiyor " + counter);
    //         yield return null;
    //     }
    //     _plate = Instantiate(plate,hand.position,Quaternion.Euler(new Vector3(-90,0,0)),hand.transform);
    //     StartCoroutine(YemegiGotur());
    // }
    // public IEnumerator YemegiGotur()
    // {
    //     animator.SetBool("tasi",true);
    //     animator.SetBool("yuru",false);
    //     counter.food.transform.SetParent(_plate.transform.GetChild(0).transform);
    //     counter.food.transform.localPosition = new Vector3(0,0.06f,0);// Vector3.zero;
    //     counter.food = null;
    //     counter.isFull = false;
    //     var _chair = FindChair();
    //     if(_chair == null)
    //     {
    //         agent.SetDestination(beklemeYeri.position);
    //         while(Vector3.Distance(agent.transform.position,beklemeYeri.transform.position) > .3f)
    //         {
    //             _chair = FindChair();
    //             if(_chair != null)
    //             {
    //                 var chair = _chair;
    //                 agent.SetDestination(chairTabakyeri.position);
    //                 break;
    //             }
    //             yield return null;
    //         }
    //     }
    //     while(_chair == null)
    //     {
    //         animator.SetBool("dur",true);
    //         animator.SetBool("tasi",false);
    //     }
    //     animator.SetBool("dur",false);
    //     animator.SetBool("tasi",true);
    //     agent.SetDestination(chairTabakyeri.transform.position);
    //     while(Vector3.Distance(agent.transform.position,chairTabakyeri.transform.position) > 1f)
    //     {
    //         yield return null;
    //     }
    //     YemegiMasayaBirak();
    // }
    // public void YemegiMasayaBirak()
    // {
    //     _plate.transform.position = chairTabakyeri.transform.position;
    //     _plate.transform.SetParent(null);
    //     this._chair.GetMusteri().YemeginGelmesi();
    //     this._chair.pizza = _pizza;
    //     this._chair.tabak = _plate;
    //     Debug.Log("coutneregit");
    //     StartCoroutine(CountereGit());
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Garson : Unit
{
    public Chair targetKirli;
    public Transform beklemeYeri;
    public BulasikToplaState bulasikToplaState;
    public BulasikGoturIdle bulasikGoturIdle;
    public BeklemeYerineGitState beklemeYerineGitState;
    public YemegiCounterdenAlState yemegiCounterdenAlState;
    public BulasikGoturState bulasikGoturState;
    public IdleState idleState;
    public TasiState tasiState;
    public YemekleBekleState yemekleBekleState;
    Animator animator;
    
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
        currState = idleState;
        
    }
    public Chair FindChair()
    {
        if(level.restaurant.yemekBekleyenChairler.Count == 0)
        {
            return null;
        }
        
        var enYakinChair = level.restaurant.yemekBekleyenChairler[0];
        for (int i = 0; i < level.restaurant.yemekBekleyenChairler.Count; i++)
        {
            if(Vector3.Distance(transform.position,enYakinChair.transform.position) > Vector3.Distance(transform.position,level.restaurant.yemekBekleyenChairler[i].transform.position))
            {
                enYakinChair = level.restaurant.yemekBekleyenChairler[i];
            }
        }
        chairTabakyeri = enYakinChair.tabakYeri;
        level.restaurant.yemekBekleyenChairler.Remove(enYakinChair);
        this._chair = enYakinChair;
        return enYakinChair;
    }
   
    public Counter FindCounter()
    {
        if(level.restaurant.yemegiHazirCounterler.Count == 0)
            return null;
        Counter nearestCounter = level.restaurant.yemegiHazirCounterler[0];
        float nearestDistance = Vector3.Distance(level.restaurant.yemegiHazirCounterler[0].transform.position,this.transform.position);
        for (int i = 0; i < level.restaurant.yemegiHazirCounterler.Count; i++)
        {
            float distance = Vector3.Distance(level.restaurant.yemegiHazirCounterler[i].transform.position,transform.position);
            if(distance < nearestDistance)
            {
                nearestCounter = level.restaurant.yemegiHazirCounterler[i];
            }
        }
        var _counter = nearestCounter;
        garsonPos = _counter.garsonPos;
        level.restaurant.yemegiHazirCounterler.Remove(nearestCounter);
        this.counter = _counter;
        return _counter;
    }
    void Update()
    {
        currState.UpdateState(action);
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

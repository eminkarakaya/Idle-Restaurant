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
        animator = GetComponent<Animator>();
        action = GetComponent<Action>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        agent.speed = moveSpeed;
        currState = idleState;
    }
    void Update()
    {
        currState.UpdateState(action);
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
    
}

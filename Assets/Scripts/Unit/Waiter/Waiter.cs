using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Waiter : Unit
{
    [HideInInspector] public Chair targetKirli;
    public Transform waitingPlace;
    // [Header("States")]
    public Restaurant restaurant;
    public CollectDishesState bulasikToplaState;
    public TakeAwayDishIdleState bulasikGoturIdle;
    public WaiterGoToWaitingPlaceState beklemeYerineGitState;
    public TakingFoodFromCounterState yemegiCounterdenAlState;
    public TakeAwayDishState bulasikGoturState;
    public IdleState idleState;
    public CarryState tasiState;
    Animator animator;
    [Header("Items")]
    public Chair chair;
    public Counter counter;
    [SerializeField] public Plate plate;
    [HideInInspector] public Plate _plate;
    [HideInInspector] public GameObject _pizza;
    [SerializeField] private float _moveSpeed = 2;
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
        level = FindObjectOfType<Level>();
        restaurant = level.restaurant;
        agent.speed = moveSpeed;
        currState = idleState;
    }
    void Update()
    {
        currState.UpdateState(action);
    }
    public void CheckDirtyPlate()
    {
        if(targetKirli == null && level.restaurant.dirtyPlates.Count !=0)
            targetKirli = level.restaurant.dirtyPlates[0];
        if(targetKirli != null)
        {
            queueImage.gameObject.SetActive(false);
            if(restaurant.availableWaiters.Contains(this))
            {
                restaurant.availableWaiters.Remove(this);
            }
            currState = bulasikToplaState;
            return;
        }
    }
    public bool CheckDeliver()
    {
        Chair tempChair = null;
        Counter tempCounter = null;
        tempChair = FindChair();
        tempCounter = FindCounter();
        if(tempChair != null && tempCounter != null)
        {
            counter = tempCounter;
            chair = tempChair;
            RemoveCounter(counter);
            RemoveChair(chair);
            restaurant.availableWaiters.Remove(this);
            queueImage.gameObject.SetActive(false);
            // this.chair = chair;
            yemegiCounterdenAlState.counter = counter;
            counter = null;
            currState = yemegiCounterdenAlState;
            return true;
        }
        return false;
    }
    public void RemoveChair(Chair chair)
    {
        restaurant.waitingForFoodChairs.Remove(chair);
    }
   
    public Chair FindChair()
    {
        if(level.restaurant.waitingForFoodChairs.Count == 0)
        {
            return null;
        }
        
        var enYakinChair = level.restaurant.waitingForFoodChairs[0];
        for (int i = 0; i < level.restaurant.waitingForFoodChairs.Count; i++)
        {
            if(Vector3.Distance(transform.position,enYakinChair.transform.position) > Vector3.Distance(transform.position,level.restaurant.waitingForFoodChairs[i].transform.position))
            {
                enYakinChair = level.restaurant.waitingForFoodChairs[i];
            }
        }
        return enYakinChair;
    }
    public void RemoveCounter(Counter counter)
    {
        restaurant.foodReadyCounters.Remove(counter);
    }
   
    public Counter FindCounter()
    {
        if(level.restaurant.foodReadyCounters.Count == 0)
            return null;
        Counter nearestCounter = level.restaurant.foodReadyCounters[0];
        float nearestDistance = Vector3.Distance(level.restaurant.foodReadyCounters[0].transform.position,this.transform.position);
        for (int i = 0; i < level.restaurant.foodReadyCounters.Count; i++)
        {
            float distance = Vector3.Distance(level.restaurant.foodReadyCounters[i].transform.position,transform.position);
            if(distance < nearestDistance)
            {
                nearestCounter = level.restaurant.foodReadyCounters[i];
            }
        }
        var _counter = nearestCounter;
        
        return _counter;
    }
    
}

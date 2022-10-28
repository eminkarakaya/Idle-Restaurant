using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Musteri : Unit
{

    public MusteriChairBekleState musteriChairBekleState;
    public MusteriEatingState customerEatingState;   
    public MusteriSiparisState customerOrderState; 
    public MusteriSittingIdleState sittingIdleState; 
    public MusteriSitToStand sitToStand; 
    public MusteriWalkState customerWalkState;   
    public MusteriStandToSitState standToSitState;  
    [Space(10)]
    public Transform parent;
    public Image earnMoneyImage;
    public Image orderImage;
    public float eatingTime;
    public Transform door;
    Animator animator;
    public Chair chair;
    public Transform placeToSit;
    void Awake()
    {
        // isReady = false;
        level = GetComponentInParent<Level>();
        action =  GetComponent<Action>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        currState = customerWalkState;
    }
    void Update()
    {
        currState.UpdateState(action);
    }
    public void MasadanKalk()
    {
        action.CustomerStand();
    }
    public void MasadanKalkState()
    {
        currState = sitToStand;
    }
    public void SiparisStateGec()
    {
        currState = customerOrderState;
    }
    public void SiparisVer()
    {
        level.restaurant.waitingForFoodChairs.Add(chair);
        currState = sittingIdleState;
    }
    public Chair FindEmptyChair()
    {
        if(level.restaurant.emptyChairs.Count == 0)
        {
            return null;
        }
        var _chair = level.restaurant.emptyChairs[0];
        chair = _chair;
        placeToSit = _chair.placeToSit;
        // Debug.Log("find " + _chair , _chair);
        level.restaurant.emptyChairs.Remove(level.restaurant.emptyChairs[0]);
        return _chair;
    }
}

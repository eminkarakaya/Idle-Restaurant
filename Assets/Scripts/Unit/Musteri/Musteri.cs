using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Musteri : Unit
{
    public Transform parent;
    public Image earnMoneyImage;
    public Image siparisImage;
    public MusteriEatingState musteriEatingState;   
    public MusteriSiparisState musteriSiparisState; 
    public MusteriSittingIdleState musteriSittingIdleState; 
    public MusteriSitToStand musteriSitToStand; 
    public MusteriWalkState musteriWalkState;   
    public MusteriStandToSitState musteriStandToSitState;  
    // public IlkSiradaBekle IlkSiradaBekle; 
    public float yemeSuresi;
    public Transform kapi;
    Animator animator;
    public Chair chair;
    public Transform oturulcakYer;
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
        currState = musteriWalkState;
    }
    void Update()
    {
        currState.UpdateState(action);
    }
    public void MasadanKalk()
    {
        action.MusteriKalk();
    }
    public void MasadanKalkState()
    {
        currState = musteriSitToStand;
    }
    public void SiparisStateGec()
    {
        currState = musteriSiparisState;
    }
    public void SiparisVer()
    {
        level.restaurant.yemekBekleyenChairler.Add(chair);
        currState = musteriSittingIdleState;
    }
    public Chair FindEmptyChair()
    {
        if(level.restaurant.emptyChairs.Count == 0)
        {
            return null;
        }
        var _chair = level.restaurant.emptyChairs[0];
        chair = _chair;
        oturulcakYer = _chair.oturulcakYer;
        level.restaurant.emptyChairs.Remove(level.restaurant.emptyChairs[0]);
        return _chair;
    }
}

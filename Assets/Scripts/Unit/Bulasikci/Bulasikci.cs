using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bulasikci : Unit
{
    public SinkBekleState waitSinkState;
    public BulasikTabakBekle waitPlateState;
    public BulasikciTabakAl takePlateState;
    public BulasikciTabakKoy putPlateState;
    public BulasikciYikaState washState;
    [Space(10)]
    public Bulasikhane scullery;
    public Sink sink;
    public BulasikCounter dishCounter;
    void Awake()
    {  
        level = FindObjectOfType<Level>();
    }
    void Start()
    {
        action = GetComponent<Action>();
        agent = GetComponent<NavMeshAgent>();
        currState = takePlateState;
        currState.StartState(action);
    }
    void Update()
    {
        currState.UpdateState(action);
    }
    
}

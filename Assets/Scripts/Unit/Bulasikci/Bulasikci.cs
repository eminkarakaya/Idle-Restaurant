using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bulasikci : Unit
{
    public Sink sink;
    public SinkBekleState sinkBekleState;
    public BulasikTabakBekle bulasikTabakBekle;
    public BulasikCounter bulasikCounter;
    public BulasikciTabakAl bulasikciTabakAl;
    public BulasikciTabakKoy bulasikciTabakKoy;
    public BulasikciYikaState bulasikciYikaState;
    public Bulasikhane bulasikhane;
    void Awake()
    {
        
        level = FindObjectOfType<Level>();
        // bulasikhane = level.bulasikhane;
        sink = bulasikhane.GetEmptySink();
    }
    void Start()
    {
        bulasikCounter = bulasikhane.FindBulasikCounter();
        action = GetComponent<Action>();
        agent = GetComponent<NavMeshAgent>();
        currState = bulasikciTabakAl;
        currState.StartState(action);
    }
    void Update()
    {
        currState.UpdateState(action);
    }
    
}

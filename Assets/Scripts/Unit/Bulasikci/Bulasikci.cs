using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bulasikci : Unit
{
    public Sink sink;
    public BulasikCounter counter;
    public BulasikTabakBekle bulasikTabakBekle;
    public BulasikCounter bulasikCounter;
    public BulasikciTabakAl bulasikciTabakAl;
    public BulasikciTabakKoy bulasikciTabakKoy;
    public BulasikciYikaState bulasikciYikaState;
    void Start()
    {
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

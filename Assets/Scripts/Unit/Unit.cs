using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    public bool eliDolumu;
    public int handSayisi;
    public List <Transform> hand;
    public Level level;
    public Slider slider;
    public Image bekleImage;
    // public bool isReady = true;
    public Action action;
    
    public QueueBekleState queueBekleState;
    public QueueState queueState;
    public NavMeshAgent agent;
    [SerializeField] private StateBase _currState;
    public StateBase currState
    {
        get => _currState;
        set{
            _currState = value;
            _currState.StartState(action);
        }
    }
}

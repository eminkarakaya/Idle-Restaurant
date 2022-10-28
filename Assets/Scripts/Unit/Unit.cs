using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    public bool isHandFull;
    public Transform hand;
    public Level level;
    public Slider slider;
    public Image queueImage;
    public Action action;
    public NavMeshAgent agent;
    [Header("States")]
    [SerializeField] private StateBase _currState;
    [Space(5)]
    public QueueBekleState queueWaitState;
    public QueueState queueState;
    public StateBase currState
    {
        get => _currState;
        set{
            _currState = value;
            _currState.StartState(action);
        }
    }
}

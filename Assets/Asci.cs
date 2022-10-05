using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Asci : MonoBehaviour
{
    Action action;
    private AsciState _currState;
    public AsciState currState
    {
        get => _currState;
        set{
            _currState = value;
            _currState.StartState(action);
        }
    }
    public CountereKoymaState countereKoymaState;
    public BuzdolabiState buzdolabiState;
    
    public PizzaAcmaState pizzaAcmaState;
    public FirinaKoymaState firinaKoymaState;
    public GameObject pizza;
    public GameObject hamur;
    Vector3 rot = new Vector3(176.847f,120.684f,-48.271f);
    Vector3 offset = new Vector3(-0.05f,120.684f,-48.271f);
    public Transform hand;
    public GameObject pan;
    public Ocak ocak;
    public PizzaAcmaCounter pizzaAcmaCounter;
    public Counter counter;
    bool isCooking;
    public Transform ocakTransform;
    public Transform counterPos;
    public NavMeshAgent agent;
    public float cookingTime;
    public float cookingTimeTemp;
    public List<Transform> targets;
    GameObject _hamur;
    GameObject _pizza;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cookingTimeTemp = cookingTime;
        StartCoroutine(GoToOven());
        targets[3] = counter.asciPos;
    }
    IEnumerator GoToOven()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            agent.SetDestination(targets[i].position);
            while(true)
            {
                yield return null;
                if(agent.remainingDistance < 0.0001f)
                {
                    
                    if(i == 0)
                    {
                        // hamuru dolaptan alma
                        _hamur = Instantiate(hamur,hand.position,Quaternion.identity);
                        _hamur.transform.SetParent(hand);
                    }
                    if(i==1)
                    {   
                        // hamuru acma
                        Destroy(_hamur);
                        _pizza = Instantiate(pizza,pizzaAcmaCounter.place.position,Quaternion.Euler(new Vector3(-90,0,0)));
                        _pizza.transform.SetParent(null);
                        yield return new WaitForSeconds(pizzaAcmaCounter.time);
                        _pizza.transform.position = hand.position;
                        _pizza.transform.SetParent(hand);
                    }
                    if(i == 2)
                    {
                        // pizzayı fırına koyma
                        _pizza.transform.SetParent(null);
                        _pizza.transform.position = ocak.tabakYeri.position;
                        _pizza.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
                        yield return new WaitForSeconds(ocak.pisirmeSuresi);
                        _pizza.transform.SetParent(hand);
                        _pizza.transform.position = hand.transform.position;
                    }
                    if(i == 3)
                    {
                        // eger counter bossa pızzayı countere koyma
                        while(counter.isFull)
                        {
                            yield return null;
                        }
                        counter.isFull = true;
                        GameManager.instance.allCounters.Add(counter);
                        Debug.Log("eklendı");
                        _pizza.transform.SetParent(null);
                        _pizza.transform.position = counter.platePos.position;
                        counter.food = _pizza;
                        _pizza.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
                    }
                    break;
                }
            }
            if(i+1 == targets.Count)
            {
                i = -1;
            }
        }
    }
}

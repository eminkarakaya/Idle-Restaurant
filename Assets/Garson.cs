using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Garson : MonoBehaviour
{
    Animator animator;
    Vector3 pizzaPos;
    public Transform hand;
    NavMeshAgent agent;
    [SerializeField] Tabak plate;
    private Tabak _plate;
    GameObject _pizza;
    [SerializeField] GameObject masa;
    [SerializeField] Counter counter;
    [SerializeField] Chair _chair;
    Transform garsonPos;
    Transform chairTabakyeri;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(CountereGit());
    }
    Chair FindChair()
    {
        if(GameManager.instance.yemekBekleyenChairler.Count == 0)
            return null;
        var chair = GameManager.instance.yemekBekleyenChairler[0];
        chairTabakyeri = chair.tabakYeri;
        GameManager.instance.yemekBekleyenChairler.Remove(GameManager.instance.yemekBekleyenChairler[0]);
        this._chair = chair;
        return chair;
    }
   
    Counter FindCounter()
    {
        if(GameManager.instance.allCounters.Count == 0)
            return null;
        Counter nearestCounter = GameManager.instance.allCounters[0];
        float nearestDistance = Vector3.Distance(GameManager.instance.allCounters[0].transform.position,this.transform.position);
        for (int i = 0; i < GameManager.instance.allCounters.Count; i++)
        {
            float distance = Vector3.Distance(GameManager.instance.allCounters[i].transform.position,transform.position);
            if(distance < nearestDistance)
            {
                nearestCounter = GameManager.instance.allCounters[i];
            }
        }
        var _counter = nearestCounter;
        garsonPos = _counter.garsonPos;
        GameManager.instance.allCounters.Remove(nearestCounter);
        this.counter = _counter;
        return _counter;
    }
    public IEnumerator CountereGit()
    {
        while(FindCounter() == null)
        {
            yield return null;
        }
        var counter = FindCounter();
        agent.SetDestination(garsonPos.position);
        while(Vector3.Distance(agent.transform.position,garsonPos.position) > 1f)
        {
            Debug.Log(Vector3.Distance(agent.transform.position,garsonPos.position));
            Debug.Log("siparisi almaya gidiyor " + counter);
            yield return null;
        }
        _plate = Instantiate(plate,hand.position,Quaternion.Euler(new Vector3(-90,0,0)),hand.transform);
        StartCoroutine(YemegiGotur());
    }
    public IEnumerator YemegiGotur()
    {
        
        counter.food.transform.SetParent(_plate.transform.GetChild(0).transform);
        counter.food.transform.localPosition = new Vector3(0,0.06f,0);// Vector3.zero;
        counter.food = null;
        counter.isFull = false;
        var _chair = FindChair();
        while(_chair == null)
        {
            _chair = FindChair();
            yield return null;
        }
        
        var chair = _chair;
        Debug.Log(chairTabakyeri);
        agent.SetDestination(chairTabakyeri.position);
        while(Vector3.Distance(agent.transform.position,chairTabakyeri.transform.position) > 1f)
        {
            Debug.Log("yemek goturuluyor " + chair);
            yield return null;
        }
        YemegiMasayaBirak();
    }
    public void YemegiMasayaBirak()
    {
        _plate.transform.position = chairTabakyeri.transform.position;
        _plate.transform.SetParent(null);
        this._chair.GetMusteri().YemeginGelmesi();
        this._chair.pizza = _pizza;
        this._chair.tabak = _plate;
        StartCoroutine(CountereGit());
    }
}

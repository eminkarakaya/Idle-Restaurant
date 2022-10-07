using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Musteri : MonoBehaviour
{
    public Level level;
    NavMeshAgent agent;
    public Chair targetChair;
    public float yemeSuresi;
    [HideInInspector] public Transform kapi;
    Animator animator;
    Transform oturulcakYer;
    Chair chair;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(MasayaGitme());
    }
    Chair FindEmptyChair()
    {
        var _chair = level.restaurant.emptyChairs[0];
        chair = _chair;
        oturulcakYer = _chair.oturulcakYer;
        targetChair = level.restaurant.emptyChairs[0];
        level.restaurant.emptyChairs.Remove(level.restaurant.emptyChairs[0]);
        return _chair;
    }
    public IEnumerator MasayaGitme()
    {
        var qwe = FindEmptyChair();
        agent.SetDestination(qwe.oturulcakYer.position);
        while(Vector3.Distance(agent.transform.position,oturulcakYer.transform.position) > .3f)
        {
            yield return null;
        }
        //masaya oturma anımasyonu
        transform.LookAt(targetChair.tabakYeri);
        animator.SetBool("yuru",false);
        agent.isStopped = true;
        animator.SetTrigger("otur");
        targetChair.SetMusteri(this);
    }
    public void YemekSiparisi()
    {
        level.yemekBekleyenChairler.Add(targetChair);
    }
    
    public void YemeginGelmesi()
    {

        animator.SetBool("eating",true);
    }
    public void MasadanKalkma()
    {

        animator.SetBool("eating",false);
        animator.SetTrigger("kalk");
        targetChair.MasadanKalkma();
    }
    public IEnumerator RestorantdanCikma()
    {
        agent.isStopped = false;
        agent.SetDestination(kapi.position);
        while( Vector3.Distance(agent.transform.position,kapi.transform.position) > 0.5f)
        {
            yield return null;
        }
        Destroy(this.gameObject);
        level.restaurant.emptyChairs.Add(chair);
    }
    
}

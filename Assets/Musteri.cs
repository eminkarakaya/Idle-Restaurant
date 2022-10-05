using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Musteri : MonoBehaviour
{
    NavMeshAgent agent;
    public Chair targetChair;
    public float yemeSuresi;
    [HideInInspector] public Transform kapi;
    Animator animator;
    Transform oturulcakYer;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(MasayaGitme());
    }
    Chair FindEmptyChair()
    {
        var _chair = GameManager.instance.emptyChairs[0];
        oturulcakYer = _chair.oturulcakYer;
        targetChair = GameManager.instance.emptyChairs[0];
        GameManager.instance.emptyChairs.Remove(GameManager.instance.emptyChairs[0]);
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
        GameManager.instance.yemekBekleyenMusteriler.Add(this);
        GameManager.instance.yemekBekleyenChairler.Add(targetChair);
    }
    
    public void YemeginGelmesi()
    {

        GameManager.instance.yemekBekleyenMusteriler.Remove(this);
        animator.SetBool("eating",true);
    }
    public void MasadanKalkma()
    {

        GameManager.instance.emptyChairs.Add(targetChair);
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
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public Animator animator;
    public Action(Animator animator)
    {
        this.animator = animator;
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void WaitWithFood()
    {
        if(animator.GetBool("tasirkenDur") == true)
            return;
        animator.SetBool("tasirkenDur",true);
        animator.SetBool("yuru",false);
        animator.SetBool("idle",false);
        animator.SetBool("tasi",false);
    }
    public void Walk()
    {
        if(animator.GetBool("yuru") == true)
            return;
        animator.SetBool("tasirkenDur",false);
        animator.SetBool("yuru",true);
        animator.SetBool("idle",false);
        animator.SetBool("tasi",false);
    }
    public void Idle()
    {
        if(animator.GetBool("idle") == true)
            return;
        animator.SetBool("tasirkenDur",false);
        animator.SetBool("yuru",false);
        animator.SetBool("idle",true);
        animator.SetBool("tasi",false);
    }
    public void Carry()
    {
        if(animator.GetBool("tasi") == true)
            return;
        animator.SetBool("tasirkenDur",false);
        animator.SetBool("yuru",false);
        animator.SetBool("idle",false);
        animator.SetBool("tasi",true);
    }
    public void CustomerWalk()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetBool("yuru",true);
        animator.SetBool("eating",false);
        animator.SetBool("idle",false);        
    }
    public void CustomerSit()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetTrigger("otur");
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("idle",false); 
    }
    public void CustomerOrder()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetTrigger("siparis");
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("idle",false); 
    }
    public void CustomerSitIdle()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("idle",true); 
    }
    public void CustomerStand()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetTrigger("kalk");
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("idle",false); 
    }
    public void CustomerEat()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetBool("idle",false); 
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("eating",true);
    }
    public void CustomerStandIdle()
    {
        animator.SetBool("ayaktaIdle",true); 
        animator.SetBool("idle",false); 
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("eating",false);
    }

}

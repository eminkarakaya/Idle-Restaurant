using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void YemekleDur()
    {
        if(animator.GetBool("tasirkenDur") == true)
            return;
        animator.SetBool("tasirkenDur",true);
        animator.SetBool("yuru",false);
        animator.SetBool("idle",false);
        animator.SetBool("tasi",false);
    }
    public void Yuru()
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
    public void Tasi()
    {
        if(animator.GetBool("tasi") == true)
            return;
        animator.SetBool("tasirkenDur",false);
        animator.SetBool("yuru",false);
        animator.SetBool("idle",false);
        animator.SetBool("tasi",true);
    }
    public void MusteriYuru()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetBool("yuru",true);
        animator.SetBool("eating",false);
        animator.SetBool("idle",false);        
    }
    public void MusteriOtur()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetTrigger("otur");
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("idle",false); 
    }
    public void MusteriSiparis()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetTrigger("siparis");
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("idle",false); 
    }
    public void MusteriOturIdle()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("idle",true); 
    }
    public void MusteriKalk()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetTrigger("kalk");
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("idle",false); 
    }
    public void MusteriYe()
    {
        animator.SetBool("ayaktaIdle",false);
        animator.SetBool("idle",false); 
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("eating",true);
    }
    public void MusteriAyaktaIdle()
    {
        animator.SetBool("ayaktaIdle",true); 
        animator.SetBool("idle",false); 
        animator.SetBool("yuru",false);
        animator.SetBool("eating",false);
        animator.SetBool("eating",false);
    }

}

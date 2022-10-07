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
    public void Yuru()
    {
        if(animator.GetBool("yuru") == true)
        {
            return;
        }
        animator.SetBool("yuru",true);
        animator.SetBool("tasi",false);
        animator.SetBool("dur",false);
        animator.SetBool("yemekleDur",false);
    }
    public void Tasi()
    {
        if(animator.GetBool("tasi") == true)
        {
            return;
        }
        animator.SetBool("yuru",false);
        animator.SetBool("tasi",true);
        animator.SetBool("dur",false);
        animator.SetBool("yemekleDur",false);
    }
    public void Dur()
    {
        if(animator.GetBool("dur") == true)
        {
            return;
        }
        animator.SetBool("yuru",false);
        animator.SetBool("tasi",false);
        animator.SetBool("dur",true);
        animator.SetBool("yemekleDur",false);
    } 
    public void YemekleDur()
    {
        if(animator.GetBool("yemekleDur") == true)
            return;
        
        animator.SetBool("yuru",false);
        animator.SetBool("tasi",false);
        animator.SetBool("dur",false);
        animator.SetBool("yemekleDur",true);
    } 
    public void AsciYemekleDur()
    {
        if(animator.GetBool("tasirkenDur") == true)
            return;
        animator.SetBool("tasirkenDur",true);
        animator.SetBool("yuru",false);
        animator.SetBool("idle",false);
        animator.SetBool("tasi",false);
    }
    public void AsciYuru()
    {
        if(animator.GetBool("yuru") == true)
            return;
        animator.SetBool("tasirkenDur",false);
        animator.SetBool("yuru",true);
        animator.SetBool("idle",false);
        animator.SetBool("tasi",false);
    }
    public void AsciIdle()
    {
        // if(animator.GetBool("idle") == true)
        //     return;
        animator.SetBool("tasirkenDur",false);
        animator.SetBool("yuru",false);
        animator.SetBool("idle",true);
        animator.SetBool("tasi",false);
    }
    public void AsciTasi()
    {
        if(animator.GetBool("tasi") == true)
            return;
        animator.SetBool("tasirkenDur",false);
        animator.SetBool("yuru",false);
        animator.SetBool("idle",false);
        animator.SetBool("tasi",true);
    }
}

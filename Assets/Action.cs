using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    Animator animator;
    void Start()
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
        {
            return;
        }
        animator.SetBool("yuru",false);
        animator.SetBool("tasi",false);
        animator.SetBool("dur",false);
        animator.SetBool("yemekleDur",true);
    } 
}

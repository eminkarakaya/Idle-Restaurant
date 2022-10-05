using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaAcmaCounter : MonoBehaviour
{
    public bool pistimi;
    public float time;
    public Transform place;
    
    public IEnumerator Pisir()
    {
        yield return new WaitForSeconds(time);
        pistimi = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutfakData : MonoBehaviour
{
    Level level;
    [SerializeField] Text firinSayisi
    ,counterSayisi,asciSayisi,counterAlCost,firinAlCost,asciAlCost,kasaAlText,kasaAlCost;

    void Start()
    {
        level= GetComponentInParent<Level>();
    }
    public void UpdateData()
    {
        
    }
}

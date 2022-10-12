using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusteriSiparisState : MusteriState
{
    
    public override void StartState(Action action)
    {
        action.MusteriSiparis();
        musteri.siparisImage.gameObject.SetActive(true);
    }
    public override void UpdateState(Action action)
    {
        
    }
}

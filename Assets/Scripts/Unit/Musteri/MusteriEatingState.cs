using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusteriEatingState : MusteriState
{
    public override void StartState(Action action)
    {
        action.MusteriYe();
        musteri.siparisImage.gameObject.SetActive(false);
    }
    public override void UpdateState(Action action)
    {
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusteriStandToSitState : MusteriState
{
    public override void StartState(Action action)
    {
        action.MusteriOtur();
        musteri.transform.LookAt(musteri.chair.tabakYeri);
    }
    public override void UpdateState(Action action)
    {
        
    }
    
}

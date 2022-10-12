using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueBekleState : StateBase
{
    public StateBase oncekiState;
    Unit unit;
    public override void StartState(Action action)
    {
        unit = GetComponentInParent<Unit>();
        unit.transform.LookAt(item.transform);
        // asci.agent.isStopped = true;
    }
    public override void UpdateState(Action action)
    {
        if(unit.TryGetComponent(out Musteri musteri))
        {
            var chair = musteri.FindEmptyChair();
            action.MusteriAyaktaIdle();
            if(item.queue[0] == unit && chair != null)
            {
                musteri.chair = chair;
                unit.bekleImage.gameObject.SetActive(false);
                musteri.currState = oncekiState;
            }
        }

        else if(item.queue[0] == unit)// && unit.isReady)
        {
            unit.bekleImage.gameObject.SetActive(false);
            // asci.agent.isStopped = false;
            unit.currState = oncekiState;
        }
    }
}

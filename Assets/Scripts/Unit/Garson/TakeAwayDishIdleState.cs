using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAwayDishIdleState : WaiterBaseState
{
    public SculleryCounter bulasikCounter;
    public Plate tabak;

    public override void StartState(Action action)
    {
        waiter.queueImage.gameObject.SetActive(true);
        item = bulasikCounter.waiterItem;
        action.WaitWithFood();
    }
    public override void UpdateState(Action action)
    {
        if(bulasikCounter.plates.Count < 1)
        {
            tabak.transform.SetParent(null);
            tabak.transform.position = bulasikCounter.platePlaces[bulasikCounter.plateCount].position;
            waiter.queueState.previousState = this;
            bulasikCounter.plates.Add(tabak);
            waiter.targetKirli = null;
            waiter.bulasikGoturState.bulasikCounter.waiterItem.UpdateQueue(waiter);
            waiter.queueImage.gameObject.SetActive(false);
            waiter.currState = waiter.beklemeYerineGitState;
            bulasikCounter.CheckCounter();
            // bulasıkcı bulasıgı al tetıklencek
        }

    }
}

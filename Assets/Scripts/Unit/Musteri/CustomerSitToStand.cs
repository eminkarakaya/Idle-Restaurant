using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusteriSitToStand : CustomerBaseState
{
    public override void StartState(Action action)
    {
        GoldAnim.instance.EarnGoldAnim2(customer.level.restaurant.earnedMoneyFromCustomer,customer.transform);
        action.CustomerWalk();
        customer.chair.MasadanKalkma();
        customer.agent.isStopped = false;
        customer.agent.SetDestination(Door.instance.kapiTransform.position);
    }
    public override void UpdateState(Action action)
    {
        if( Vector3.Distance(customer.agent.transform.position,Door.instance.kapiTransform.position) > 0.5f)
        {
            return;
        }
        
        // musteri.level.restaurant.emptyChairs.Add(musteri.chair);
        
        // if(item.queue.Count !=0)
        //     item.queue[0].currState = item.queue[0].GetComponent<Musteri>().musteriWalkState;
        ObjectPool.instance.pools[2].pooledObjects.Enqueue(customer.parent.gameObject);
        customer.placeToSit = null;
        customer.chair = null;
        customer.parent.gameObject.SetActive(false);
    }
}
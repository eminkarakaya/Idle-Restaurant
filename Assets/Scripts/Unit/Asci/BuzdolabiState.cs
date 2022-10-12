using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzdolabiState : AsciState
{
    Transform _buzdolabiTransform;
    GameObject _hamur;
    GameObject hamur;

    public override void StartState(Action action)
    {
        hamur = asci.hamur;
        _buzdolabiTransform = asci.buzdolabi;
        
        asci.agent.SetDestination(_buzdolabiTransform.position);
        action.Yuru();
    }
    public override void UpdateState(Action action)
    {
        if(Vector3.Distance(asci.agent.transform.position,_buzdolabiTransform.position) > 0.6f)
        {
            return;
        }
        asci.pizzaAcmaState.hamur = Instantiate(hamur,asci.hand[asci.handSayisi-1].position,Quaternion.identity,asci.hand[asci.handSayisi-1]);
        asci.currState = asci.pizzaAcmaState;
        
    }
}

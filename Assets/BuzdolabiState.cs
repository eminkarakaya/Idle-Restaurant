using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzdolabiState : AsciState
{
    Transform _buzdolabiTransform;
    GameObject _hamur;
    GameObject hamur;
    Asci asci;

    public override void StartState(Action action)
    {
        asci = GetComponentInParent<Asci>();
        _hamur = Instantiate(hamur,asci.hand.position,Quaternion.identity,asci.hand);
        asci.agent.SetDestination(_buzdolabiTransform.position);
    }
    public override AsciState UpdateState(Action action)
    {
        if(Vector3.Distance(asci.agent.transform.position,_buzdolabiTransform.position) > 0.3f)
        {
            return this;
        }
        _hamur = Instantiate(hamur,asci.hand.position,Quaternion.identity,asci.hand);
        return asci.pizzaAcmaState;
        
    }
}

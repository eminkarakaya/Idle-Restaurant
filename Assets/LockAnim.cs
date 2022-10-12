using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LockAnim : MonoBehaviour
{
    Vector3 rot = new Vector3(0,180,0);
    void Start()
    {
        transform.DORotate(rot,3f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
    
}

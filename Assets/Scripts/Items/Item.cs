using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Item : MonoBehaviour
{
    public int tabakSayisi;
    public int tabakKapasitesi;
    public List<GameObject> ascilar;
    public List<GameObject> musteriler;
    public List<GameObject> bulasikcilar;
    public List<Transform> createdQueueTransform;
    [SerializeField] private List<Unit> _queue;
    public List<Unit> queue { get => _queue; set { _queue = value;}}
    public Transform asciYeri;
    public List <Transform> tabakYerleri;
    void Start()
    {
        createdQueueTransform.Add(asciYeri);
    }
    public void CreateQueue(Unit unit)
    {
        if(!queue.Contains(unit))
        {
            queue.Add(unit);
        }
    }
    public void UpdateQueue(Unit unit)
    {
        if(queue.Contains(unit))
        {
            queue.Remove(unit);
        }
        for (int i = 0; i < queue.Count; i++)
        {
            Debug.Log(queue[i].currState + " pojssıpsadspı Oncekıstates");
            
            // queue[i].queueState.oncekiState = queue[i].currState;
            queue[i].queueState.queuePlace = createdQueueTransform[i].position;
            queue[i].queueState.isUpdate = true;
            queue[i].currState = queue[i].queueState;
        }
    }
}

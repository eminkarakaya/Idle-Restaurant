using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Item : MonoBehaviour
{
    public int plateCount;
    public List<GameObject> chefs;
    public List<GameObject> customers;
    public List<GameObject> dishwashers;
    public List<Transform> createdQueueTransform;
    [SerializeField] private List<Unit> _queue;
    public List<Unit> queue { get => _queue; set { _queue = value;}}
    public Transform chefPlace;
    public List <Transform> platePlaces;
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
            
            // queue[i].queueState.oncekiState = queue[i].currState;
            queue[i].queueState.queuePlace = createdQueueTransform[i].position;
            queue[i].queueState.isUpdate = true;
            queue[i].currState = queue[i].queueState;
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Item : MonoBehaviour
{
    public List<Vector3> createdQueueTransform;
    [SerializeField] private List<Unit> _queue;
    public List<Unit> queue { get => _queue; set { _queue = value;}}
    public float siraSikligi { get; set; }
    public Transform asciYeri;
    public Transform tabakYeri;
    void Start()
    {
        createdQueueTransform.Add(asciYeri.position);
    }
    public Vector3 CreateQueue(Unit unit)
    {
        queue.Add(unit);
        if(queue.Count == 0)
        {
            return asciYeri.position;
        }
        else
        {
            Debug.Log(transform.position + " "  + createdQueueTransform[createdQueueTransform.Count-1]);
            var dir = this.transform.position - createdQueueTransform[createdQueueTransform.Count-1];
            dir = dir.normalized;
            createdQueueTransform.Add(dir*siraSikligi);
            return (dir*siraSikligi);
        }
    }
    public void UpdateQueue(Unit unit)
    {
        if(queue.Contains(unit))
        {
            queue.Remove(unit);
            createdQueueTransform.RemoveAt(createdQueueTransform.Count-1);
            for (int i = 0; i < queue.Count; i++)
            {
                if(queue[i].TryGetComponent(out Asci asci))
                {
                    asci.currState = asci.queueState;
                }
            }
        }
    }
}

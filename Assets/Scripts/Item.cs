using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Item : MonoBehaviour
{
    public List<Transform> createdQueueTransform;
    [SerializeField] private List<Unit> _queue;
    public List<Unit> queue { get => _queue; set { _queue = value;}}
    public float siraSikligi { get; set; }
    public Transform asciYeri;
    public Transform tabakYeri;
    void Start()
    {
        siraSikligi = .1f;
        createdQueueTransform.Add(asciYeri);
    }
    public Vector3 CreateQueue(Unit unit)
    {
        if(!queue.Contains(unit))
            queue.Add(unit);
        if(queue.Count == 0)
        {
            return asciYeri.position;
        }
        else
        {
            var dir = this.transform.position - createdQueueTransform[createdQueueTransform.Count-1].position;
            // dir = dir.normalized;

            var _transform = Instantiate(new GameObject("kekw"),(this.transform.position - dir),Quaternion.identity);
            createdQueueTransform.Add(_transform.transform);
            return (dir*siraSikligi);
        }
    }
    public void UpdateQueue(Unit unit)
    {
        if(queue.Contains(unit))
        {
            queue.Remove(unit);
        }
            createdQueueTransform.RemoveAt(createdQueueTransform.Count-1);
            for (int i = 0; i < queue.Count; i++)
            {
                Debug.Log(queue[i].transform.parent);
                queue[i].GetComponent<Asci>().currState = queue[i].GetComponent<Asci>().queueState;

                // {
                //     Debug.Log(asci + " asci");
                //     asci.currState = asci.queueState;
                // }
            }
    }
}

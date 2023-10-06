using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectDishesState : WaiterBaseState
{
    // public Chair FindBosTabak()
    // {
    // musteri.level.restaurant.emptyChairs.Add(musteri.chair);
    // }
    AudioSource audioSource;
    [SerializeField] private AudioClip puttingTableAudio;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public override void StartState(Action action)
    {
        action.Walk();
        if(waiter.level.restaurant.dirtyPlates.Count != 0)
            waiter.targetKirli = waiter.level.restaurant.dirtyPlates[0];
        waiter.level.restaurant.dirtyPlates.Remove(waiter.targetKirli);
        waiter.level.restaurant.emptyChairs.Add(waiter.targetKirli);
        // StartCoroutine(GameManager.instance.SetDestinationCouroutine(garson.targetKirli.transform.position,gar,this));
    }
    public override void UpdateState(Action action)
    {
        waiter.agent.SetDestination(waiter.targetKirli.transform.position);
        if(Vector3.Distance(waiter.transform.position,waiter.targetKirli.transform.position) < .4f)
        {
            waiter.targetKirli.plate.transform.SetParent(waiter.transform);
            waiter.targetKirli.plate.transform.position = waiter.hand.position;
            audioSource.PlayOneShot(puttingTableAudio);
            waiter.bulasikGoturState.tabak = waiter.targetKirli.plate;
            waiter.currState = waiter.bulasikGoturState;
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WaiterWaitingWithFoodState : WaiterBaseState
// {
//     void Start()
//     {
//         waiter = GetComponentInParent<Waiter>();
//     }
//     public override void StartState(Action action)
//     {
//         action.WaitWithFood();
//     }
//     public override void UpdateState(Action action)
//     {
//         var chair = waiter.FindChair();
//         waiter.RemoveChair(waiter.chair);
//         if(chair != null)
//         {
//             waiter.currState =  waiter.tasiState;
//         }
//         return;
//     }
// }

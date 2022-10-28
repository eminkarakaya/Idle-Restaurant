// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;
// using System;

// public class IdleMoney : MonoBehaviour
// {
//     public string url = "http://worldtimeapi.org/api/ip";
    
//     public void LoadData()
//     {
//         sDate = sonGirisTarihi;
//         GecenSureyiHesapla();
//     }
//     public void SaveData(LevelData data)
//     {
//         // _Time time = NewTime.GetTime();
        
//     }
//     public int GecenSureyiHesapla()
//     {
//         string dateOld = sDate;
//         if(string.IsNullOrEmpty(dateOld))
//         {
//             Debug.Log("firstgame");
//         }
//         else
//         {
//             // _Time time = NewTime.GetTime();
//             DateTime _dateNow = Convert.ToDateTime(DateTime.Now);
//             DateTime _dateOld = Convert.ToDateTime(sDate);
//             TimeSpan diff = _dateNow.Subtract(_dateOld);
//             return (int) diff.TotalSeconds;
//         }
//         return 0;
//     }
// }

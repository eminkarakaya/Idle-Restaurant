using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelData 
{
    public int levelIndex;
    public bool isUnlock;
    public int kitchenCount;
    public bool [] kitchenIsLocked = new bool [10];
    public int [] firinSayisi = new int[10];
    public int [] counterSayisi = new int[10];
    public int [] pizzaCounterSayisi = new int[10];
    public int [] asciSayisi = new int[10];
    public int [] asciCost = new int[10];
    public int [] firinCost = new int[10];
    public int [] counterCost = new int[10];
    public int [] pizzaCounterCost = new int[10];
    public int [] mutfakIndexleri = new int[10];
    public int [] bulasikciSayisi = new int[10];
    public int [] sinkSayisi = new int[10];
    public int [] bulasikCounterSayisi = new int[10];
    public bool [] bulasikhaneIsLocked = new bool [10];
    public int masaSayisi;
    public int garsonSayisi;
    public float garsonHizi;
    public int garsonUcreti;
    public int masaUcreti;
    public int garsonMoveUcreti;
    public int musteriSiklikUcreti;
    public int bulasikhaneCount;
    
    public LevelData(Level level)
    {
        kitchenIsLocked = new bool [10];
        firinSayisi = new int[10];
        counterSayisi = new int[10];
        pizzaCounterSayisi = new int[10];
        asciSayisi = new int[10];
        asciCost = new int[10];
        firinCost = new int[10];
        counterCost = new int[10];
        pizzaCounterCost = new int[10];
        mutfakIndexleri = new int[10];
        bulasikciSayisi = new int[10];
        sinkSayisi = new int[10];
        bulasikCounterSayisi = new int[10];
        bulasikhaneIsLocked = new bool [10];
        levelIndex = level.levelIndex;
        isUnlock = level.isUnlock;
        kitchenCount = level.kitchens.Count;
        for (int i = 0; i < level.kitchens.Count; i++)
        {
            kitchenIsLocked[level.levelIndex] = level.kitchens[i].isLocked;
            mutfakIndexleri[level.levelIndex] = (level.kitchens[i].mutfakIndex);
            firinSayisi[level.levelIndex] = (level.kitchens[i].firinSayisi);
            counterSayisi[level.levelIndex] = (level.kitchens[i].counterSayisi);
            pizzaCounterSayisi[level.levelIndex] = (level.kitchens[i].pizzaCounterSayisi);
            asciSayisi[level.levelIndex] = (level.kitchens[i].asciSayisi);
            kitchenIsLocked[level.levelIndex] = (level.kitchens[i].isLocked);
        }
        for (int i = 0; i < level.bulasikhane.Count; i++)
        {
            sinkSayisi[level.levelIndex] = level.bulasikhane[i].sinkSayisi;
            bulasikCounterSayisi[level.levelIndex] = level.bulasikhane[i].bulasikCounterSayisi;
            bulasikhaneIsLocked[level.levelIndex] = level.bulasikhane[i].isLocked;
            bulasikciSayisi[level.levelIndex] = level.bulasikhane[i].bulasikciSayisi;
        }
        masaSayisi = level.restaurant.tumMasalar.Count;
        garsonSayisi = level.restaurant.tumGarsonlar.Count;
        
        garsonUcreti = level.restaurant.garsonUcreti.GetGold();
        masaUcreti = level.restaurant.masaUcreti.GetGold();
        garsonMoveUcreti = level.restaurant.garsonMoveUcreti.GetGold();
        musteriSiklikUcreti = level.restaurant.musteriSiklikUcreti.GetGold();
    }
}
// }
// [System.Serializable]
// public class KitchenDataSave
// {
// public bool isLocked;
// public int firinSayisi;
// public int counterSayisi;
// public int pizzaCounterSayisi;
// public int asciSayisi;
// public KitchenDataSave (Kitchen kitchen)
// {
//     isLocked = kitchen.isLocked;
//     firinSayisi = kitchen.firinSayisi;
//     counterSayisi = kitchen.counterSayisi;
//     pizzaCounterSayisi = kitchen.pizzaCounterSayisi;
//     asciSayisi = kitchen.asciSayisi;
// }

// }
// [System.Serializable]
// public class RestaurantDataSave
// {
// public int chairSayisi;
// public int garsonSayisi;

// public int garsonUcreti;
// public int masaUcreti;
// public int garsonMoveUcreti;
// public int musteriSiklikUcreti;
// }
// [System.Serializable]
// public class BulasikhaneDataSave
// {
// public int sinkSayisi;
// public int bulasikciSayisi;
// public int bulasikCounterSayisi;

// public int bulasikCounterCost;
// public int sinkCost;
// public int bulasikciCost;
// }
[System.Serializable]

public class GameData
{
    public int para;
    public LevelData [] levelDatas = new LevelData[10];
    public int currLevel;
    public Level [] tumLeveller = new Level[10];
}
    // public GameData(LevelData [] levelDatas)
    // {
    //     for (int i = 0; i < tumLeveller.Length; i++)
    //     {
    //         for (int j = 0; j < levelDatas[i].kitchenCount; j++)
    //         {
    //             levelDatas[i].asciSayisi[j] = tumLeveller[i].kitchens[j].asciSayisi;
    //             levelDatas[i].pizzaCounterSayisi[j] = tumLeveller[i].kitchens[j].pizzaCounterSayisi;
    //             levelDatas[i].counterSayisi[j] = tumLeveller[i].kitchens[j].counterSayisi;
    //             levelDatas[i].firinSayisi[j] = tumLeveller[i].kitchens[j].firinSayisi;
    //             var temp3 = tumLeveller[i].kitchens[j].firinSayisi;
    //             tumLeveller[i].kitchens[j].firinSayisi = 0;
    //             for (int k = 0; k < temp3; k++)
    //             {
    //                 tumLeveller[i].kitchens[j].FirinSatinAl(false);
    //             }
    //             var temp2 = tumLeveller[i].kitchens[j].counterSayisi;
    //             tumLeveller[i].kitchens[j].counterSayisi = 0;
    //             for (int k = 0; k < temp2; k++)
    //             {
    //                 tumLeveller[i].kitchens[j].KasaSatinAl(false);
    //             }
    //             var temp1 = tumLeveller[i].kitchens[j].pizzaCounterSayisi;
    //             tumLeveller[i].kitchens[j].pizzaCounterSayisi = 0;
    //             for (int k = 0; k < temp1; k++)
    //             {
    //                 tumLeveller[i].kitchens[j].PizzaCounterSatinAl(false);
    //             }
    //             var temp = tumLeveller[i].kitchens[j].asciSayisi;
    //             tumLeveller[i].kitchens[j].asciSayisi = 0;
    //             for (int k = 0; k < temp; k++)
    //             {
    //                 tumLeveller[i].kitchens[j].AsciSatinAl(false);
    //             }
    //         }
        //     Debug.Log(gameData.levelDatas[currLevel.levelIndex].kitchenCount);
        //     for (int i = 0; i < gameData.levelDatas[currLevel.levelIndex].kitchenCount; i++)
        //     {
        //         levelDatas[i] gameData.levelDatas[currLevel.levelIndex].asciSayisi[i] = currLevel.kitchens[i].asciSayisi;
        //         gameData.levelDatas[currLevel.levelIndex].counterSayisi[i] = currLevel.kitchens[i].counterSayisi;
        //         gameData.levelDatas[currLevel.levelIndex].firinSayisi[i] = currLevel.kitchens[i].firinSayisi;
        //         // gameData.levelDatas[level.levelIndex].firinSayisi[i] = currLevel.kitchens[i].firinSayisi;
        // }   
        
    
    // public List<int> acilanLeveller;
    // public int levelIndex;
    // public bool isUnlock;
    // public float moveSpeed;
    // public List<bool> isLocked = new List<bool>();
    // public List<int> firinSayisi = new List<int>();
    // public List<int> counterSayisi = new List<int>();
    // public List<int> pizzaCounterSayisi = new List<int>();
    // public List<int> asciSayisi = new List<int>();
    // public int kitchenCount;
    // public List<int> mutfakIndexleri = new List<int>();
    // public int chairSayisi;
    // public int garsonSayisi;
    // public int garsonSikligi;
    // public int garsonUcreti;
    // public int masaUcreti;
    // public int garsonMoveUcreti;
    // public int musteriSiklikUcreti;
    // public GameData(Level [] level)
    // {
    //     for (int i = 0; i < level.Length; i++)
    //     {
    //         levelIndex = level[i].levelIndex;
    //         isUnlock = level[i].isUnlock;
    //         mutfakIndexleri.Clear();
    //         firinSayisi.Clear();
    //         counterSayisi.Clear();
    //         pizzaCounterSayisi.Clear();
    //         asciSayisi.Clear();
    //         isLocked.Clear();
    //         kitchenCount = level[i].kitchens.Count;
    //         for (int j = 0; i < level[j].kitchens.Count; j++)
    //         {
    //             mutfakIndexleri.Add(level[i].kitchens[j].mutfakIndex);
    //             firinSayisi.Add(level[i].kitchens[j].firinSayisi);
    //             counterSayisi.Add(level[i].kitchens[j].counterSayisi);
    //             pizzaCounterSayisi.Add(level[i].kitchens[j].pizzaCounterSayisi);
    //             asciSayisi.Add(level[i].kitchens[j].asciSayisi);
    //             isLocked.Add(level[i].kitchens[j].isLocked);
    //         }
    //         chairSayisi = level[i].restaurant.tumMasalar.Count;
    //         garsonSayisi = level[i].restaurant.tumGarsonlar.Count;
            
    //         moveSpeed = level[i].restaurant.moveSpeed;
            
    //         garsonUcreti = level[i].restaurant.garsonUcreti.GetGold();
    //         masaUcreti = level[i].restaurant.masaUcreti.GetGold();
    //         garsonMoveUcreti = level[i].restaurant.garsonMoveUcreti.GetGold();
    //         musteriSiklikUcreti = level[i].restaurant.musteriSiklikUcreti.GetGold();
            
    //     }
    




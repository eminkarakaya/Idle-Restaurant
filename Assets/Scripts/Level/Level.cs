using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Level : MonoBehaviour
{
    public LevelData data;
    public LevelManager levelManager;
    private int _gold;
    private TextMeshProUGUI goldText;
    public int levelIndex;
    public bool isUnlock;
    public Restaurant restaurant;
    public List<Kitchen> kitchens;
    public List<Bulasikhane> bulasikhane;
    public ParkinLot parkinLot;

    void OnEnable()
    {
        data = GameManager.instance.gameData.levelDatas[levelIndex];
        // GameManager.instance.gameData.levelDatas[levelIndex] = data; // data = SaveSystem.LoadLevel();
        Debug.Log("loaded");
    }
    void OnApplicationQuit()
    {
        GameManager.instance.gameData.levelDatas[levelIndex] = data;
    }
    void Start()
    {
        restaurant = GetComponentInChildren<Restaurant>();
        bulasikhane.Add(GetComponentInChildren<Bulasikhane>());
        // kitchens.Add(GetComponentInChildren<Kitchen>());
        levelManager = FindObjectOfType<LevelManager>();
        LoadLevel();
    }
    public void SetGold(int value)
    {
        _gold+= value;
        goldText.text = _gold.ToString();
    }
    public int GetGold()
    {
        return _gold;
    }

    public void SaveLevel()
    {
        // SaveSystem.SaveLevel(this);
    }
    public void LoadLevel()
    {
        restaurant.moveSpeed = GameManager.instance.gameData.levelDatas[levelIndex].garsonHizi;
        for (int i = 0; i < GameManager.instance.gameData.levelDatas[levelIndex].kitchenCount; i++)
        {
            // kitchens[i].isLocked = GameManager.instance.gameData.levelDatas[levelIndex].kitchenIsLocked[i];
            // kitchens[i].counterSayisi = GameManager.instance.gameData.levelDatas[levelIndex].counterSayisi[i];
            // kitchens[i].pizzaCounterSayisi = GameManager.instance.gameData.levelDatas[levelIndex].pizzaCounterSayisi[i];
            // kitchens[i].firinSayisi = GameManager.instance.gameData.levelDatas[levelIndex].firinSayisi[i];
            // kitchens[i].asciSayisi = GameManager.instance.gameData.levelDatas[levelIndex].asciSayisi[i];
            var temp3 = GameManager.instance.gameData.levelDatas[levelIndex].firinSayisi[i];
            kitchens[i].firinSayisi = 0;
            for (int j = 0; j < temp3; j++)
            {
                kitchens[i].FirinSatinAl(false);
            }
            var temp = GameManager.instance.gameData.levelDatas[levelIndex].asciSayisi[i];
            kitchens[i].asciSayisi = 0;
            for (int j = 0; j < temp; j++)
            {
                kitchens[i].AsciSatinAl(false);
            }
            var temp2 = GameManager.instance.gameData.levelDatas[levelIndex].counterSayisi[i];
            kitchens[i].counterSayisi = 0;
            for (int j = 0; j < temp2; j++)
            {
                kitchens[i].KasaSatinAl(false);
            }
            var temp1 = GameManager.instance.gameData.levelDatas[levelIndex].pizzaCounterSayisi[i];
            kitchens[i].pizzaCounterSayisi = 0;
            for (int j = 0; j < temp1; j++)
            {
                kitchens[i].PizzaCounterSatinAl(false);
            }
        }
        for (int i = 0; i < GameManager.instance.gameData.levelDatas[levelIndex].bulasikhaneCount; i++)
        {   
            bulasikhane[i].isLocked = GameManager.instance.gameData.levelDatas[levelIndex].bulasikhaneIsLocked[i];
            var sinktemp = GameManager.instance.gameData.levelDatas[levelIndex].sinkSayisi[i];
            bulasikhane[i].sinkSayisi = 0;
            for (int j = 0; j < sinktemp; j++)
            {
                bulasikhane[i].BulasikciSatinAl(false);
            }
        }
        var tempchair =  GameManager.instance.gameData.levelDatas[levelIndex].masaSayisi;
        restaurant.masaSayisi = 0;
        for (int i = 0; i < tempchair; i++)
        {
            restaurant.MasaSatinAl(false);
        }
        var garsontemp = GameManager.instance.gameData.levelDatas[levelIndex].garsonSayisi;
        restaurant.garsonSayisi = 0;
        for (int i = 0; i < garsontemp; i++)
        {
            restaurant.GarsonSatinAl(false);
        }
        levelIndex = GameManager.instance.gameData.levelDatas[levelIndex].levelIndex;
        isUnlock = GameManager.instance.gameData.levelDatas[levelIndex].isUnlock;
        restaurant.moveSpeed = GameManager.instance.gameData.levelDatas[levelIndex].garsonHizi;
      
        // garsonSayisi = data.garsonlar;
        // asciSayisi = data.asciSayisi;
        // masaSayisi = data.masaSayisi;
        // asciKapasitesi = data.asciKapasitesi;
        // garsonKapasitesi = data.garsonKapasitesi;
    }
    public void MapiAc()
    {
        GameManager.instance.MapiAc();
    }
}

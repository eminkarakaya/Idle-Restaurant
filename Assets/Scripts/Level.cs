using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Chair> yemekBekleyenChairler;
    public List<Counter> yemegiHazirCounterler;    
    public int levelIndex;
    public bool isUnlock;
    public Restaurant restaurant;
    public Kitchen kitchen;
    
    void OnEnable()
    {
        LoadLevel();
        Debug.Log("loaded");
    }
    void OnDisable()
    {
        SaveLevel();
        Debug.Log("saved");
    }
    void Start()
    {
        restaurant = GetComponentInChildren<Restaurant>();
        kitchen = GetComponentInChildren<Kitchen>();
    }
    public void SaveLevel()
    {
        SaveSystem.SaveLevel(this);
    }
    public void LoadLevel()
    {
        LevelData data = SaveSystem.LoadLevel();
        levelIndex = data.levelIndex;
        isUnlock = data.isUnlock;
        // garsonSayisi = data.garsonlar;
        // asciSayisi = data.asciSayisi;
        // masaSayisi = data.masaSayisi;
        // asciKapasitesi = data.asciKapasitesi;
        // garsonKapasitesi = data.garsonKapasitesi;
    }
    
}

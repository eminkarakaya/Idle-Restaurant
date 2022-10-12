using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Level : MonoBehaviour
{
    private int _gold;
    private TextMeshProUGUI goldText;
    public int levelIndex;
    public bool isUnlock;
    public Restaurant restaurant;
    public List<Kitchen> kitchens;
    public Bulasikhane bulasikhane;
    
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
        // kitchens = GetComponentInChildren<Kitchen>();
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

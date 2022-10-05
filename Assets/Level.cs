using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Transform> garsonBeklemeYerleri;
    public int levelIndex;
    public bool isUnlock;
    public int garsonSayisi;
    public int asciSayisi;
    public int masaSayisi;
    public int asciKapasitesi;
    public int garsonKapasitesi;
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
    public void SaveLevel()
    {
        SaveSystem.SaveLevel(this);
    }
    public void LoadLevel()
    {
        LevelData data = SaveSystem.LoadLevel();
        levelIndex = data.levelIndex;
        isUnlock = data.isUnlock;
        garsonSayisi = data.garsonlar;
        asciSayisi = data.asciSayisi;
        masaSayisi = data.masaSayisi;
        asciKapasitesi = data.asciKapasitesi;
        garsonKapasitesi = data.garsonKapasitesi;
    }
}

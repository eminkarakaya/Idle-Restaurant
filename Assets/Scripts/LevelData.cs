using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData 
{
    public int levelIndex;
    public bool isUnlock;
    public int garsonlar;
    public int asciSayisi;
    public int masaSayisi;
    public int asciKapasitesi;
    public int garsonKapasitesi;
    public LevelData(Level level)
    {
        levelIndex = level.levelIndex;
        isUnlock = level.isUnlock;
        // garsonlar = level.garsonSayisi;
        // asciSayisi = level.asciSayisi;
        // masaSayisi = level.masaSayisi;
        // asciKapasitesi = level.asciKapasitesi;
        // garsonKapasitesi = level.garsonKapasitesi;
    }
}

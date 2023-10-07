using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem 
{
    public static string SAVE_FOLDER = Application.persistentDataPath;

    public static void Init()
    {
        // if(!Directory.Exists(SAVE_FOLDER))
        // {
        //     Directory.CreateDirectory(SAVE_FOLDER);
        // }
    }
    public static void Save(string saveString)
    {
        PlayerPrefs.SetString(SAVE_FOLDER,saveString);
        PlayerPrefs.Save();
    }
    public static string Load()
    {
        if(PlayerPrefs.HasKey(SAVE_FOLDER))
        {
            string saveString = PlayerPrefs.GetString(SAVE_FOLDER);
            return saveString;
        }
        return null;
    }
}

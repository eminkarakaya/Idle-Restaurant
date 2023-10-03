using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem 
{
    public static string SAVE_FOLDER = "test2";

    public static void Init()
    {
        // if(!Directory.Exists(SAVE_FOLDER))
        // {
        //     Directory.CreateDirectory(SAVE_FOLDER);
        // }
    }
    public static void Save(string saveString)
    {
        // File.WriteAllText(SAVE_FOLDER + "/save.txt",saveString);
        PlayerPrefs.SetString(SAVE_FOLDER,saveString);
        // PlayerPrefs.Save();
// #if UNITY_EDITOR
//         if(saveString == PlayerPrefs.GetString(SAVE_FOLDER))
//         {
//             Debug.Log("UNITY_EDITOR");
//         }
// #endif
//         if(saveString == PlayerPrefs.GetString(SAVE_FOLDER))
//         {
//             Debug.Log("sföğaofkağfkpsa");
//         }
    }
    public static string Load()
    {
        if(PlayerPrefs.HasKey(SAVE_FOLDER))
        {
            Debug.Log("LOADED");
            string saveString = PlayerPrefs.GetString(SAVE_FOLDER);
            // string saveString = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            return saveString;
        }
        return null;
    }
}

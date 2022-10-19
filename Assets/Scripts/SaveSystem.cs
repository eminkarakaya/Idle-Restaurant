using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveSystem 
{
    // public static void SaveLevel(Level level)
    // {
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     string path = Application.persistentDataPath + "/level.txt";
    //     FileStream stream = new FileStream(path,FileMode.Create);

    //     // KitchenDataSave dataSave = new KitchenDataSave();

    //     LevelData data = new LevelData(level);
        
    //     formatter.Serialize(stream,data);
    //     stream.Close();
    // }
    // public static LevelData LoadLevel()
    // {
    //     string path = Application.persistentDataPath + "/level.txt";
    //     if(File.Exists(path))
    //     {
    //         BinaryFormatter formatter = new BinaryFormatter();
    //         FileStream stream = new FileStream(path,FileMode.Open);

    //         LevelData data =  formatter.Deserialize(stream) as LevelData;
    //         stream.Close();

    //         return data;
    //     }
    //     else
    //     {
    //         Debug.Log("save file not found");
    //         return null;
    //     }
        
    // }
    // public static void SaveGame(LevelData [] levelDatas)
    // {
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     string path = Application.persistentDataPath + "/game.txt";
    //     FileStream stream = new FileStream(path,FileMode.Create);

    //     // KitchenDataSave dataSave = new KitchenDataSave();

    //     GameData data = new GameData(levelDatas);
        
    //     formatter.Serialize(stream,data);
    //     stream.Close();
    // }
    // public static GameData LoadGame(Level level)
    // {
    //     string path = Application.persistentDataPath + "/game.txt";
    //     if(File.Exists(path))
    //     {
    //         BinaryFormatter formatter = new BinaryFormatter();
    //         FileStream stream = new FileStream(path,FileMode.Open);

    //         GameData data =  formatter.Deserialize(stream) as GameData;
    //         stream.Close();

    //         return data;
    //     }
    //     else
    //     {
    //         Debug.Log("save file not found");
    //         return null;
    //     }
    // }
    // public static void SaveKitchen(Kitchen kitchen)
    // {
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     string path = Application.persistentDataPath + "/kitchens.txt";
    //     FileStream stream = new FileStream(path,FileMode.Create);

    //     KitchenDataSave data = new KitchenDataSave(kitchen);        
    //     formatter.Serialize(stream,data);
    //     stream.Close();
    // }
    // public static KitchenDataSave LoadKitchen()
    // {
    //     string path = Application.persistentDataPath + "/kitchens.txt";
    //     if(File.Exists(path))
    //     {
    //         BinaryFormatter formatter = new BinaryFormatter();
    //         FileStream stream = new FileStream(path,FileMode.Open);

    //         KitchenDataSave data =  formatter.Deserialize(stream) as KitchenDataSave;
    //         stream.Close();

    //         return data;
    //     }
    //     else
    //     {
    //         Debug.Log("save file not found");
    //         return null;
    //     }
    // }
}

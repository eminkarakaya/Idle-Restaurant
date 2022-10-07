using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{

    public static void SaveLevel(Level level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";
        FileStream stream = new FileStream(path,FileMode.Create);

        LevelData data = new LevelData(level);
        
        formatter.Serialize(stream,data);
        stream.Close();
    }
    public static LevelData LoadLevel()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);

            LevelData data =  formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("save file not found");
            return null;
        }
    }
}

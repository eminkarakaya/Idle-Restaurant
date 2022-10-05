using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSaveLoad : MonoBehaviour
{
    void OnEnable()
    {
        Load();
    }
    void OnDisable()
    {
        Save();
    }
    public Envanter envanter;
    public void Load()
    {
        envanter = JsonUtility.FromJson<Envanter>(PlayerPrefs.GetString("Data"));
    }
    public void Save()
    {
        var data = JsonUtility.ToJson(envanter);
        PlayerPrefs.SetString("Data" , data);
    }
}
[System.Serializable]
public class Envanter 
{
    public GameObject obje;
    public int sayi;
}
[System.Serializable]
public class EnvanterItems
{
    public List<Envanter> items = new List<Envanter>();
}

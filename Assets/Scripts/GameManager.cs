using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public GameObject garsonPrefab;
    public GameObject asciPrefab;
    public List<int> acilanLeveller;
    public static GameManager instance {get;private set;}
    void Awake()
    {
        instance = this;
    }
}

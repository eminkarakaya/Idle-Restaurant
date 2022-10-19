using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public List<Level> allLevels;        
    public List<Level> unlockedLevels;        
    public List<int> unlockedLevelsIndex;      
    [SerializeField] private TextMeshProUGUI paraText;
    public int idleMoney;    
    [SerializeField] private TextMeshProUGUI idleMoneyText;
    public int gold;
    [SerializeField] private TextMeshProUGUI goldText;  
    public GameObject garsonPrefab;
    public GameObject asciPrefab;
    public GameObject motorPrefab;
    public GameObject bulasikciPrefab;
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class GameManager : MonoBehaviour 
{
    [SerializeField] private int _para = 99999999;
    [SerializeField] private TextMeshProUGUI paraText;
    public int idleMoney;    
    [SerializeField] private TextMeshProUGUI idleMoneyText;
    public int gold;
    [SerializeField] private TextMeshProUGUI goldText;
    public TextParse [] allTextParse;
    public GameObject garsonPrefab;
    public GameObject asciPrefab;
    public GameObject motorPrefab;
    public List<int> acilanLeveller;
    public static GameManager instance {get;private set;}
    void Awake()
    {
        instance = this;
        SetPara(0);
        idleMoneyText.text = CaclText(idleMoney);
        goldText.text = CaclText(gold);
    }
    public void SetPara(int value)
    {
        _para += value;
        paraText.text = CaclText(_para );
    }
    public int GetPara()
    {
        return _para;
    }
    public static string CaclText(float value)
    {
        if(value < 1000)
        {
            return String.Format("{0:0.0}",value);
        }
        else if(value >= 1000 && value < 1000000)
        {
            return String.Format("{0:0.0}",value /1000 ) + "k";
        }
        else if(value >= 1000000 && value < 1000000000)
        {
            return String.Format("{0:0.0}",value /1000000) + "m";
        }
        else if(value >= 1000000000 && value < 1000000000000)
        { 
            return String.Format("{0:0.0}",value / 1000000000) + "b";
        }
        else if(value >= 1000000000000 && value < 1000000000000000)
        { 
            return String.Format("{0:0.0}",value / 1000000000000) + "t";
        }
        else if(value >= 1000000000000000 && value < 1000000000000000000)
        { 
            return String.Format("{0:0.0}",value / 1000000000000000) + "aa";
        }
        else if(value >= 1000000000000000000)
        { 
            return String.Format("{0:0.0}",value / 1000000000000000) + "ab";
        }
        return value.ToString();
    }
}

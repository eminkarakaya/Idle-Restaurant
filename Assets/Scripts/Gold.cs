using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Gold : MonoBehaviour
{
    [SerializeField] private int _gold;
    public TextParse textParse;
    void Start()
    {
        SetGold(0);
    }
    public void SetGold(int value)
    {
        _gold += value;
        textParse.Check(_gold);
        textParse.text = GameManager.CaclText(_gold);     
    }
    public int GetGold()
    {
        return _gold ;
    }
}

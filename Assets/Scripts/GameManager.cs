using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class GameManager : MonoBehaviour 
{
    
    public Level currLevel;
    public GameData gameData;
    public Image fader;
    [SerializeField] private int _para = 99999999;
    [SerializeField] private TextMeshProUGUI paraText;
    public int idleMoney;    
    [SerializeField] private TextMeshProUGUI idleMoneyText;
    public int gold;
    [SerializeField] private TextMeshProUGUI goldText;
    public TextParse [] allTextParse;
    public List<int> acilanLeveller;
    public Level [] tumLeveller = new Level[10];
    public static GameManager instance {get;private set;}
    void Awake()
    {
        instance =this;        
        var objects = FindObjectsOfType<GameManager>();
        if(objects.Length > 1)
        {
            Destroy(objects[0].gameObject);   
        }
        DontDestroyOnLoad(gameObject);
        SetPara(0);
        idleMoneyText.text = CaclText(idleMoney);
        goldText.text = CaclText(gold);
        Load();
        Debug.Log(GameManager.instance.tumLeveller[0]);
    }
    void OnDisable()
    {
        Save(currLevel);    
    }
    public void Save(Level level)
    {
        gameData.para = _para;
        gameData.currLevel = currLevel.levelIndex;
        gameData.levelDatas[level.levelIndex].kitchenCount = currLevel.kitchens.Count;
        for (int i = 0; i < gameData.levelDatas[level.levelIndex].kitchenCount; i++)
        {

            gameData.levelDatas[level.levelIndex].kitchenIsLocked[i] = currLevel.kitchens[i].isLocked;
            gameData.levelDatas[level.levelIndex].asciSayisi[i] = currLevel.kitchens[i].asciSayisi;
            gameData.levelDatas[level.levelIndex].counterSayisi[i] = currLevel.kitchens[i].counterSayisi;
            gameData.levelDatas[level.levelIndex].firinSayisi[i] = currLevel.kitchens[i].firinSayisi;
            gameData.levelDatas[level.levelIndex].pizzaCounterSayisi[i] = currLevel.kitchens[i].pizzaCounterSayisi;
            gameData.levelDatas[level.levelIndex].pizzaCounterCost[i] = currLevel.kitchens[i].pizzaCounterCost.GetGold();
            gameData.levelDatas[level.levelIndex].firinCost[i] = currLevel.kitchens[i].firinCost.GetGold();
            gameData.levelDatas[level.levelIndex].counterCost[i] = currLevel.kitchens[i].counterCost.GetGold();
            gameData.levelDatas[level.levelIndex].asciCost[i] = currLevel.kitchens[i].asciCost.GetGold();
        }
        gameData.levelDatas[level.levelIndex].bulasikhaneCount = currLevel.bulasikhane.Count;
        gameData.levelDatas[level.levelIndex].garsonSayisi = currLevel.restaurant.tumGarsonlar.Count;
        gameData.levelDatas[level.levelIndex].garsonHizi = currLevel.restaurant.moveSpeed;
        gameData.levelDatas[level.levelIndex].masaSayisi = currLevel.restaurant.masaSayisi;
        // for (int i = 0; i < gameData.levelDatas[level.levelIndex].bulasikhaneCount; i++)
        // {
        //     gameData.levelDatas[level.levelIndex].bulasikhaneIsLocked[i] = currLevel.bulasikhane[i].isLocked;
        //     gameData.levelDatas[level.levelIndex].bulasikciSayisi[i] = currLevel.bulasikhane[i].allBulasikci.Count;
        //     gameData.levelDatas[level.levelIndex].bulasikCounterSayisi[i] = currLevel.bulasikhane[i].allBulasikCounter.Count;
        //     gameData.levelDatas[level.levelIndex].sinkSayisi[i] = currLevel.bulasikhane[i].allSinks.Count;
        // }
        
        
        var data = JsonUtility.ToJson(gameData); 
        PlayerPrefs.SetString("data",data);

        Debug.Log("saved");
    }
    public void Load()
    {        
        gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString("data"));
        _para = gameData.para;
    }
    public void SetPara(int value)
    {
        _para += value;
        paraText.text = CaclText(_para );
        allTextParse = FindObjectsOfType<TextParse>();
        for (int i = 0; i < allTextParse.Length; i++)
        {
            allTextParse[i].Check(allTextParse[i].GetComponent<Gold>().GetGold());
        }
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
    public void MapiAc()
    {
        StartCoroutine(FadeScene(0,1,1));
    }
    public IEnumerator FadeScene(int level,float duration , float waitTime)
    {
        // fader.transform.position = Vector3.zero;
        float passed = 0f;
        fader.gameObject.SetActive(true);
        while(passed < duration)
        {
            passed += Time.deltaTime;
            fader.color = new Color(0,0,0,Mathf.Lerp(0,1,passed));
            yield return null;
        }
        AsyncOperation ao = SceneManager.LoadSceneAsync(level);
        while(!ao.isDone)
        {
            yield return null;
        }
        SceneManager.LoadScene(level);
        yield return new WaitForSeconds(waitTime);
        passed = 0;
        fader.gameObject.SetActive(false);
        while(passed < duration)
        {
            passed += Time.deltaTime;
            fader.color = new Color(0,0,0,Mathf.Lerp(1,0,passed));
            yield return null;
        }
    }
    
}

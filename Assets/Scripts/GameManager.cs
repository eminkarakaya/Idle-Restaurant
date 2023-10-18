using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] private float saveFrequency = 3f;
    public bool resetData;
    public GameData gameData;
    public Image fader;
    [SerializeField] private int _money;
    [SerializeField] private TextMeshProUGUI paraText;
    public float idleMoneyPerSec;
    [SerializeField] public TextMeshProUGUI idleMoneyText;
    public int gold;
    [SerializeField] private TextMeshProUGUI goldText;
    public TextParse [] allTextParse;
    public static GameManager instance = null;
    void Awake(){
        if(instance== null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
       
        
        Load();
        
        idleMoneyText.text = CaclText(idleMoneyPerSec);
        goldText.text = CaclText(gold);
    }
    private void Start()
    {
        
        if(gameData.languageIndex != null)
        {
            LanguageManager.Instance.ChangeLanguage((int)gameData.languageIndex);
        }
        StartCoroutine(SaveCoroutine());
    }

    private void OnApplicationQuit() {
        
        Save();
    }
    private IEnumerator SaveCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(saveFrequency);
            Save();
        }
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    public void OpenMap()
    {
        Level level = FindObjectOfType<Level>();
        level.SaveLevel();
        StartCoroutine(FadeScene(1,1,1));
    }
    
    
    public void SetIdleMoneyText(float moneyPerSecond)
    {
        idleMoneyPerSec = moneyPerSecond;
        idleMoneyText.text = CaclText(idleMoneyPerSec) + "$/s";
    }
    public void Save()
    {
        Level level = FindObjectOfType<Level>();
        if(level != null)
        {
            level.SaveLevel();
        }
        var data = JsonUtility.ToJson(gameData);
        SaveSystem.Save(data);
    }
    public void Load()
    {
        SaveSystem.Init();
        if(resetData)
        {
            if(gameData.levelData.Length == 0)
            {
                gameData = new GameData();
            }
            return;
        }
        string saveString = SaveSystem.Load();

        if(saveString != null)
        {
            gameData = JsonUtility.FromJson<GameData>(saveString);
        }
        if(gameData.levelData.Length == 0)
        {
            gameData = new GameData();
        }
        _money = gameData.money;
        paraText.text = CaclText(_money );
    }
    public void SetMoney(int value)
    {
        _money += value;
        paraText.text = CaclText(_money );
        allTextParse = FindObjectsOfType<TextParse>();
        for (int i = 0; i < allTextParse.Length; i++)
        {
            allTextParse[i].Check(allTextParse[i].GetComponent<Gold>().GetGold());
        }
    }
    public int GetMoney()
    {
        return _money;
    }
    public static string CaclText(float value)
    {
        if(value == 0)
        {
            return "0";
        }
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
        
    public static void LoadScene(int index,float dur = 1,float waitTime = 1)
    {
        instance.StartCoroutine(instance.FadeScene(index,dur,waitTime));
    }
    public IEnumerator FadeScene(int level,float duration , float waitTime)
    {
        float passed = 0f;
        fader = transform.GetChild(0).GetComponent<Image>();
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
    public IEnumerator SetDestinationCouroutine(Vector3 destination, Unit unit , StateBase state)
    {
        while (true)
        {
            if(state != unit.currState)
                break;
            yield return new WaitForSeconds(.2f);
            unit.agent.SetDestination(destination);
        }
    }
}

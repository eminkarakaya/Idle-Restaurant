using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class GameManager : MonoBehaviour
{
    int lastSceneIndex;
    public Level currLevel;
    public GameData gameData;
    public Image fader;
    [SerializeField] private int _money;
    [SerializeField] private TextMeshProUGUI paraText;
    public int idleMoney;
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
        SetMoney(0);
        idleMoneyText.text = CaclText(idleMoney);
        goldText.text = CaclText(gold);
        Load();
    }
    private void Start()
    {
        SceneManager.LoadScene(lastSceneIndex);
    }
    void OnDisable()
    {
        Save();
    }
    public void Save()
    {
        var data = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("data",data);

    }
    public void Load()
    {
        if(!PlayerPrefs.HasKey("data"))
            return;
        gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString("data"));
        _money = gameData.para;
        lastSceneIndex = gameData.lastSceneIndex;
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
    public void OpenMap()
    {
        StartCoroutine(FadeScene(1,1,1));
    }
    public static void LoadScene(int index,float dur = 1,float waitTime = 1)
    {
        instance.StartCoroutine(instance.FadeScene(index,dur,waitTime));
    }
    public IEnumerator FadeScene(int level,float duration , float waitTime)
    {
        // fader.transform.position = Vector3.zero;
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
                // yield return null;
            yield return new WaitForSeconds(.2f);
            unit.agent.SetDestination(destination);
        }
    }
}

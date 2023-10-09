using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapLevel : MonoBehaviour
{
    float time =0;
    [SerializeField] int levelIndex;
    [SerializeField] int levelSceneIndex;
    public bool isUnlocked;
    public Text unlockCostText;
    public TextMeshProUGUI goldPerSecText;
    public GameObject levelImage;
    public GameObject lockImage;
    public Level level;
    [SerializeField] Gold gold;
    void Start()
    {
        // unlockCostText.text = "0";
        isUnlocked = GameManager.instance.gameData.levelData[levelIndex].isUnlock;
        if(isUnlocked)
        {
            unlockCostText.gameObject.SetActive(false);
            goldPerSecText.gameObject.SetActive(true);
            StartCoroutine(CalcLevelIdleGold());    
            levelImage.SetActive(true);
        }
        else   
        {
            lockImage.SetActive(true);

        }
    }
    public void UnlockLevel()
    {
        if(gold.GetGold() <= GameManager.instance.GetMoney())
        {
            GameManager.instance.gameData.levelData[levelIndex].isUnlock = true;
            isUnlocked = true;
            GameManager.instance.SetMoney(-gold.GetGold());
            levelImage.SetActive(true);
            lockImage.SetActive(false);
            goldPerSecText.text = "0";
            GameManager.instance.Save();
        }
    }   
    public void SelectLevel()
    {
        GameManager.LoadScene(levelSceneIndex);
    }
    IEnumerator CalcLevelIdleGold()
    {        
        if(String.IsNullOrEmpty(GameManager.instance.gameData.levelData[levelIndex].lastLoginDate))
        {
            goldPerSecText.text = "0";
            yield break;
        }
        while(true)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                DateTime _dateNow = Convert.ToDateTime(DateTime.Now);
                DateTime _dateOld = Convert.ToDateTime(GameManager.instance.gameData.levelData[levelIndex].lastLoginDate);
                TimeSpan diff = _dateNow.Subtract(_dateOld);

                goldPerSecText.text = GameManager.CaclText((GameManager.instance.gameData.levelData[levelIndex].goldEarnedPerSec *(int) diff.TotalSeconds)/10);
                time = 2;
            }
            yield return null;
        }
    }
}

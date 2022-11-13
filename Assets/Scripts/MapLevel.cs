using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapLevel : MonoBehaviour
{
    float time =0;
    [SerializeField] int levelIndex;
    [SerializeField] int levelSceneIndex;
    public bool isUnlocked;
    public Text earnedGoldText;
    public Text unlockCostText;
    public GameObject levelImage;
    public GameObject lockImage;
    public Level level;
    [SerializeField] Gold gold;
    void Start()
    {
        isUnlocked = GameManager.instance.gameData.levelData[levelIndex].isUnlock;
        if(isUnlocked)
        {
            StartCoroutine(CalcLevelIdleGold());    
            levelImage.SetActive(true);
        }
        else   
            lockImage.SetActive(true);
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
        }
    }   
    public void SelectLevel()
    {
        GameManager.LoadScene(levelSceneIndex);
    }
    IEnumerator CalcLevelIdleGold()
    {        
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
                Debug.Log(GameManager.CaclText((GameManager.instance.gameData.levelData[levelIndex].goldEarnedPerSec * (int)diff.TotalSeconds) / 10));
                earnedGoldText.text = GameManager.CaclText((GameManager.instance.gameData.levelData[levelIndex].goldEarnedPerSec *(int) diff.TotalSeconds)/10);
                time = 2;
            }
            yield return null;
            // Debug.Log((GameManager.instance.gameData.levelData[levelIndex].goldEarnedPerSec * diff.TotalSeconds)/10);
        }
    }
}

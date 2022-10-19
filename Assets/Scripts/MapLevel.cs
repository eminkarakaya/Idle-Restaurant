using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapLevel : MonoBehaviour
{
    [SerializeField] int levelIndex;
    [SerializeField] int levelSceneIndex;
    public bool isUnlocked;
    public Text earnedGoldText;
    public Text unlockCostText;
    public GameObject levelImage;
    public GameObject lockImage;
    public Level level;
    Gold gold;
    GameManager gameManager;
    void Start()
    {
        if(isUnlocked)
            levelImage.SetActive(true);
        else   
            lockImage.SetActive(true);
    }
    public void UnlockLevel()
    {
        if(gold.GetGold() <= GameManager.instance.GetPara())
        {
            isUnlocked = true;
            GameManager.instance.SetPara(-gold.GetGold());
            levelImage.SetActive(true);
            lockImage.SetActive(false);
        }
    }    
    public void SelectLevel()
    {
        Debug.Log("kekekw");
        StartCoroutine(GameManager.instance.FadeScene(levelSceneIndex,1,1));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class Level : MonoBehaviour
{
    [SerializeField] private AudioClip environmentClip, restaurantClip;

    //public GameObject bulasikhaneTask, mutfakTask, restoranTask;
    AudioSource audioSource;
    public Sprite orderSprite;
    public GameObject pastry;
    public GameObject pizza;
    public GameObject idleMoneyCanvas;
    public TextMeshProUGUI earnedIdleMoneyText;
    public TextMeshProUGUI passedTimeText;
    public string lastLoginDate;
    public LevelData levelData;
    public LevelManager levelManager;
    private int _gold;
    private TextMeshProUGUI goldText;
    public int levelIndex;
    public bool isUnlock;
    public Restaurant restaurant;
    public List<Kitchen> allKitchens;
    public List<Kitchen> unlockedKitchens;
    public List<Scullery> allSculleries;
    public List<Scullery> unlockedSculleries;
    public List <Takeaway> allParkinLots;
    public List <Takeaway> unlockedParkinLots;

    
    void Awake()
    {
        restaurant = GetComponentInChildren<Restaurant>();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        allKitchens = FindObjectsOfType<Kitchen>().ToList();
        allSculleries = FindObjectsOfType<Scullery>().ToList();
        allParkinLots = FindObjectsOfType<Takeaway>().ToList();
        restaurant = FindObjectOfType<Restaurant>();
        levelManager = FindObjectOfType<LevelManager>();
        LoadLevel();
        
        lastLoginDate = levelData.lastLoginDate;
        earnedIdleMoneyText.text = GameManager.CaclText((CalcPassingTime()*(CalculateEarnedMoneyOfPerSeconds())/10)) + "$";
        TimeSpan t = TimeSpan.FromSeconds( CalcPassingTime() );
        string str;
        if(CalcPassingTime() > 86400)
        {
            str = t.ToString(@"dd\hh\:mm\:ss\:fff");
        }
        else if(CalcPassingTime() > 3600)
        {
            str = t.ToString(@"hh\:mm\:ss\:");
        }
        else if(CalcPassingTime() > 60)
        {
            str = t.ToString(@"mm") + " dk " + t.ToString(@"ss") + " sn";
        }
        else
        {
            str = t.ToString(@"ss") + " sn";
        }
        audioSource.clip = environmentClip;
        audioSource.Play();
        passedTimeText.text = str;
        if(IsRestaurantReady() == false)
        {
            idleMoneyCanvas.SetActive(false);
        }
        
        RestaurantReady(true);
        GameManager.instance.SetIdleMoneyText(CalculateEarnedMoneyOfPerSeconds());
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    public void SaveLevel()
    {
        foreach (var item in allKitchens)
        {
            item.SaveKitchen();
        }
        foreach (var item in allSculleries)
        {
            item.SaveScullery();
        }
        restaurant.SaveRestaurant();
        // levelData = new LevelData{
            
        // };
        levelData.goldEarnedPerSec = CalculateEarnedMoneyOfPerSeconds();
        levelData.isUnlock = isUnlock;
        levelData.lastLoginDate = DateTime.Now.ToString();
        GameManager.instance.gameData.levelData[levelIndex] = levelData;
    }
    public void SetGold(int value)
    {
        _gold+= value;
        goldText.text = _gold.ToString();
    }
    public int GetGold()
    {
        return _gold;
    }
    
    public void OpenMap()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        SaveLevel();
        GameManager.instance.Save();
        gameManager.OpenMap();
    }
    public void LoadLevel()
    {
        levelData = GameManager.instance.gameData.levelData[levelIndex];
        if(levelData == null ||levelData.kitchenData == null || levelData.kitchenData.Length == 0)
        {
            levelData = new LevelData(allSculleries.Count,allParkinLots.Count,allKitchens.Count);
        }
        isUnlock = levelData.isUnlock;
        if(isUnlock)
        {
            idleMoneyCanvas.SetActive(true);
        }
        if(levelIndex == 0)
        {
            isUnlock = true;
        }

        foreach (var item in allKitchens)
        {
            item.LoadKitchen();
        }
        foreach (var item in allSculleries)
        {
            item.LoadScullery();
        }
        restaurant.LoadRestaurant();
    }
    
    [ContextMenu("CalculateEarnedMoneyOfPerSeconds")]
    public float CalculateEarnedMoneyOfPerSeconds()
    {
        List<float> sort = new List<float>();
        var kitchenTotal = 0f;
        if (allKitchens.Count == 0)
        {
            return 0;
        }
        var kitchenCount = 0;
        for (int i = 0; i < allKitchens.Count; i++)
        {
            if(allKitchens[i].PizzaMakingTime() == 0)
                continue;
            kitchenTotal += allKitchens[i].PizzaMakingTime();
            kitchenCount ++;
        }
        if (kitchenCount == 0)
            kitchenTotal = 0;
        kitchenTotal = (kitchenTotal / kitchenCount) / kitchenCount;
        var sculleryTotal = 0f;
        for (int i = 0; i < allSculleries.Count; i++)
        {
            sculleryTotal += allSculleries[i].PizzaMakingTime();
        }        
        if (sculleryTotal == 0)
        {
            return 0;
        }
        if(allSculleries.Count == 0)
        {
            sculleryTotal = 0;
        }
        else
            sculleryTotal = (sculleryTotal/allSculleries.Count) /allSculleries.Count;
        sort.Add(kitchenTotal);
        sort.Add(restaurant.PizzaDistributingTime());
        sort.Add(sculleryTotal);
        sort.Sort();
        if (float.IsNaN(sort[0]))
        {
            return 0;
        }
        if(restaurant.earnedMoneyFromCustomer == 0)
            return 0;
        
        return restaurant.earnedMoneyFromCustomer / sort[0];
    }
    private IEnumerator WaitOneFrame()
    {
        yield return new WaitForEndOfFrame();
        idleMoneyCanvas.SetActive(false);
    }
    public void IdleMoneyCanvasActive()
    {
        StartCoroutine(WaitOneFrame());
        StartCoroutine(GoldAnim.Instance.EarnGoldAnim((int)(CalcPassingTime()*(CalculateEarnedMoneyOfPerSeconds())/10),20,GameManager.instance.idleMoneyText.transform));
    }
    public void ShowRewardedAd()
    {
        AdManager.Instance.LoadRewardedAd(()=>
        {
            StartCoroutine(WaitOneFrame());
            StartCoroutine(GoldAnim.Instance.EarnGoldAnim((int)((CalcPassingTime()*(CalculateEarnedMoneyOfPerSeconds())/10)*2),20,GameManager.instance.idleMoneyText.transform));
        });
    }
    public int CalcPassingTime()
    {
        string dateOld = lastLoginDate;
        if(string.IsNullOrEmpty(dateOld))
        {
        }
        else
        {
            // _Time time = NewTime.GetTime();
            DateTime _dateNow = Convert.ToDateTime(DateTime.Now);
            DateTime _dateOld = Convert.ToDateTime(lastLoginDate);
            TimeSpan diff = _dateNow.Subtract(_dateOld);
            return (int) diff.TotalSeconds;
        }
        
        return 0;
    }
    // customer create
    public bool RestaurantReady(bool isStart)
    {
        if(restaurant.isLocked)
            return false;
        var kitchenCount = 0;
        for (int i = 0; i < allKitchens.Count; i++)
        {
            if(!allKitchens[i].isLocked && !allKitchens[i].kuryeMutfagimi)
            {
                kitchenCount++;
            }
        }
        if(kitchenCount == 0)
            return false;
        var sculleryCount =0;
        for (int i = 0; i < allSculleries.Count; i++)
        {
            if(!allSculleries[i].isLocked)
                sculleryCount ++;
        }
        if(sculleryCount == 0)
            return false;
        if(isStart)
        {
            StartCoroutine (CustomerCreator.instance.CustomerCreate());
            audioSource.clip = restaurantClip;
            audioSource.Play();
            return true;
        }
        if(sculleryCount >= 1 && kitchenCount >= 1 && !restaurant.isLocked)
        {
            audioSource.clip = restaurantClip;
            audioSource.Play();
            StartCoroutine (CustomerCreator.instance.CustomerCreate());
            return true;
        }
        
        audioSource.clip = environmentClip;
        audioSource.Play();
        return false;
    }
    // only check
    public bool IsRestaurantReady()
    {
        if(restaurant.isLocked)
            return false;
        var kitchenCount = 0;
        for (int i = 0; i < allKitchens.Count; i++)
        {
            if(!allKitchens[i].isLocked && !allKitchens[i].kuryeMutfagimi)
            {
                kitchenCount++;
            }
        }
        if(kitchenCount == 0)
            return false;
        var sculleryCount =0;
        for (int i = 0; i < allSculleries.Count; i++)
        {
            if(!allSculleries[i].isLocked)
                sculleryCount ++;
        }
        if(sculleryCount == 0)
            return false;
        return sculleryCount >= 1 && kitchenCount >= 1 && !restaurant.isLocked;
    }
    public void OffAllColliders()
    {
        foreach (var item in allKitchens)
        {
            item.GetComponent<Collider>().enabled = false;
        }
        foreach (var item in allSculleries)
        {
            item.GetComponent<Collider>().enabled = false;
        }
        foreach (var item in allParkinLots)
        {
            item.GetComponent<Collider>().enabled = false;
        }
        restaurant.GetComponent<Collider>().enabled = false; 
    }
    private IEnumerator OnAllCollidersCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (var item in allKitchens)
        {
            item.GetComponent<Collider>().enabled = true;
        }
        foreach (var item in allSculleries)
        {
            item.GetComponent<Collider>().enabled = true;
        }
        foreach (var item in allParkinLots)
        {
            item.GetComponent<Collider>().enabled = true;
        }
        restaurant.GetComponent<Collider>().enabled = true; 
    } 
    public void OnAllColliders()
    {
        // StartCoroutine(OnAllCollidersCoroutine());
         foreach (var item in allKitchens)
        {
            item.GetComponent<Collider>().enabled = true;
        }
        foreach (var item in allSculleries)
        {
            item.GetComponent<Collider>().enabled = true;
        }
        foreach (var item in allParkinLots)
        {
            item.GetComponent<Collider>().enabled = true;
        }
        restaurant.GetComponent<Collider>().enabled = true; 
    }
}

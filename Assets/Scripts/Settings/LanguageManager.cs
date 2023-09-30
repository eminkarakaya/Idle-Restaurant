using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageManager : Singleton<LanguageManager>
{
    [SerializeField] private int selectedIndex;
    [SerializeField] private GameObject[] languages; 
    public void ChangeLanguage(int index)
    {
        foreach (var item in languages)
        {
            item.transform.GetChild(0).gameObject.SetActive(false);
        }
        languages[index].transform.GetChild(0).gameObject.SetActive(true);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}

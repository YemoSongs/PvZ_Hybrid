﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



/// <summary>
/// 语言本地化管理器
/// </summary>
public class LocalizationManager : SingletonAutoMono<LocalizationManager>
{

    private Dictionary<string, string> localizedText;
    private string missingTextString = "Localized text not found";



    //加载本地化语言
    public void LoadLocalizedText(string languageName)
    {
        if(localizedText != null)
            localizedText.Clear();
        localizedText = new Dictionary<string, string>();
        LocalizationData data;
        ABResMgr.Instance.LoadResAsync<TextAsset>("localizationtext", languageName, (res) =>
        {

            if (res != null)
            {
                try
                {
                    data = JsonUtility.FromJson<LocalizationData>(res.text);
                    foreach (var item in data.items)
                    {
                        localizedText.Add(item.key, item.value);
                    }
                }
                 catch (Exception e)
                {
                    Debug.LogError("JSON parse error: " + e.Message);
                }

                Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
            }
            else
            {
                Debug.Log("Data is Null");
            }
            
        });

    }

    /// <summary>
    /// 从加载的语言中获取对应的文本
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            return missingTextString;
        }
    }

    /// <summary>
    /// 切换语言
    /// </summary>
    /// <param name="languageCode"></param>
    public void SetLanguage(string languageCode)
    {
        switch (languageCode)
        {
            case "English":
                LoadLocalizedText("English");
                break;
            case "Chinese":
                LoadLocalizedText("Chinese");
                break;
            default:
                LoadLocalizedText("English");
                break;
        }

        // 刷新所有本地化文本
        foreach (LocalizedText text in FindObjectsOfType<LocalizedText>())
        {
            text.Start();
        }
    }



}

[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}


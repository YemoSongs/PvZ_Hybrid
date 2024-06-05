﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 本地化语言显示组件（挂载到需要显示本地化语言的TextMeshProUGUI的物体上）
/// </summary>
public class LocalizedText : MonoBehaviour
{
    public string key;

    TextMeshProUGUI text;

    public void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = LocalizationManager.Instance.GetLocalizedValue(key);
        }
    }

    private void OnEnable()
    {
        if (text != null)
        {
            text.text = LocalizationManager.Instance.GetLocalizedValue(key);
        }
    }

}

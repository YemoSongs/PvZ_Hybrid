﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar: MonoBehaviour
{
    [SerializeField] private Image img_Background;
    [SerializeField] private Image img_Foreground;
    [SerializeField] private RectTransform img_Bar;

    private float width;

    public event Action OnProgressEnd;


    private void Start()
    {
        width = img_Background.rectTransform.rect.width;
    }


    public void SetProgress(float progress)
    {
        img_Bar.anchoredPosition = new Vector3(progress * width,55,0);
        img_Bar.rotation = Quaternion.AngleAxis(progress*720,Vector3.back);
        img_Foreground.fillAmount = progress;
        if(img_Foreground.fillAmount == 1)
        {
            OnProgressEnd?.Invoke();
        }
    }

}

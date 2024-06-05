﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustratedGuidePanel : BasePanel
{
    #region 自定义字段，属性

    #endregion    

    public override void HideMe()
    {
        
    }

    public override void ShowMe()
    {
        
    }

    #region 自定义函数

    #endregion
    
    #region 控件事件监听
    void Btn_Plants_OnClick()
    {
        
    }

    void Btn_Zombies_OnClick()
    {
        
    }

    void Btn_Close_OnClick()
    {
        UIMgr.Instance.HidePanel<IllustratedGuidePanel>();
    }


    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_Plants":
                Btn_Plants_OnClick();
                break;

            case "Btn_Zombies":
                Btn_Zombies_OnClick();
                break;

            case "Btn_Close":
                Btn_Close_OnClick();
                break;

            default:
                break;
        }
    }

    protected override void ToggleValueChange(string toggleName, bool value)
    {
        switch (toggleName)
        {
            default:
                break;
        }
    }

    protected override void SliderValueChange(string sliderName, float value)
    {
        switch (sliderName)
        {
            default:
                break;
        }
    }
    #endregion
}
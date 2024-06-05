﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_Resolution
{
    R_FullScreen,
    R_1920x1080,
    R_1366x768,
    R_960x540
}


public class OptionsPanel : BasePanel
{

    #region 自定义字段，属性
    //我的B站主页
    private string url = "https://space.bilibili.com/3493139241241123?spm_id_from=333.1365.0.0";


    private float musicValue = 0;
    private float soundValue = 0;
    private int resolution = 0;
    private int language = 0;
    private bool canSetSpeed = false;

    #endregion




    public override void HideMe()
    {

    }



    public override void ShowMe()
    {
        
    }

    #region 自定义函数
    // 设置分辨率的方法
    public void SetResolution(E_Resolution e_Resolution)
    {

        switch (e_Resolution)
        {
            case E_Resolution.R_FullScreen:
                Screen.SetResolution(1920, 1080, true);
                print("SetResolution(1920, 1080, true)");
                break;
            case E_Resolution.R_1920x1080:
                Screen.SetResolution(1920, 1080, false);
                print("SetResolution(1920, 1080, false)");
                break;
            case E_Resolution.R_1366x768:
                Screen.SetResolution(1366, 768, false);
                print("SetResolution(1366, 768, false)");
                break;
            case E_Resolution.R_960x540:
                Screen.SetResolution(960, 540, false);
                print("SetResolution(960, 540, false)");
                break;
        }

    }


    private void SetLanguage(int language)
    {
        switch(language)
        {
            case 0:
                LocalizationManager.Instance.SetLanguage("Chinese");
                break;
            case 1:
                LocalizationManager.Instance.SetLanguage("English");
                break;
        }
    }

    #endregion



    #region 事件监听相关
    void Btn_Confirm_OnClick()
    {

        //保存并修改选项的数据
        //。。。。。

        SetResolution((E_Resolution)resolution);

        SetLanguage(language);


        //关闭当前选项面板
        UIMgr.Instance.HidePanel<OptionsPanel>();
    }
    void Btn_JumpToWeb_OnClick()
    {
        //跳转到url
        Application.OpenURL(url);
    }

    void Slider_MusicValue_ChangeValue(float value)
    {
        MusicMgr.Instance.ChangeBKMusicValue(value);
        musicValue = value;
    }

    void Slider_SoundValue_ChangeValue(float value)
    {
        MusicMgr.Instance.ChangeSoundValue(value);
        soundValue = value;
    }

    void Dropdown_Resolution_ChangeValue(int value)
    {        
        resolution = value;
    }
    void Dropdown_Language_ChangeValue(int value)
    {
        language = value;
    }

    void Toggle_SetSpeed_ChangeValue(bool value)
    {
        canSetSpeed = value;
    }




    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_Confirm":
                Btn_Confirm_OnClick();
                break;
            case "Btn_JumpToWeb":
                Btn_JumpToWeb_OnClick();
                break;
        }
    }

    protected override void SliderValueChange(string sliderName, float value)
    {
        switch(sliderName)
        {
            case "Slider_MusicValue":
                Slider_MusicValue_ChangeValue(value);
                break;
            case "Slider_SoundValue":
                Slider_SoundValue_ChangeValue(value);
                break;
        }
    }

    protected override void ToggleValueChange(string toggleName, bool value)
    {
        switch (toggleName)
        {
            case "Toggle_SetSpeed":
                Toggle_SetSpeed_ChangeValue(value);
                break;
        }
    }

    protected override void DropdownValueChange(string dropdownName, int value)
    {
        switch (dropdownName)
        {
            case "Dropdown_Resolution":
                Dropdown_Resolution_ChangeValue(value);
                break;
            case "Dropdown_Language":
                Dropdown_Language_ChangeValue(value);
                break;
        }
    }





    #endregion


}

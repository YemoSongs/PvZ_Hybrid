using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : BasePanel
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
    void Btn_Close_OnClick()
    {

        SceneMgr.Instance.LoadSceneAsyn("GameLaunchScene", () =>
        {
            UIMgr.Instance.HidePanel<LevelPanel>();
            UIMgr.Instance.ShowPanel<MenuPanel>();
        });

    }

    void Btn_Confirm_OnClick()
    {
        //根据选中的关卡来加载关卡

        UIMgr.Instance.HidePanel<LevelPanel>();
    }


    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_Close":
                Btn_Close_OnClick();
                break;

            case "Btn_Confirm":
                Btn_Confirm_OnClick();
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
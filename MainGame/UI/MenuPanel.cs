﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : BasePanel
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


    #region 事件监听相关


    void Btn_Quit_OnClick()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
    void Btn_Options_OnClick()
    {
        //UIMgr.Instance.HidePanel<MenuPanel>();
        UIMgr.Instance.ShowPanel<OptionsPanel>();
    }
    void Btn_Help_OnClick()
    {
        //UIMgr.Instance.HidePanel<MenuPanel>();
        UIMgr.Instance.ShowPanel<HelpPanel>();
    }
    void Btn_AdventureMode_OnClick()
    {
        //UIMgr.Instance.HidePanel<MenuPanel>();
        UIMgr.Instance.ShowPanel<TipsPanel>(E_UILayer.Top,(panel)=>
        {
            panel.ShowContent("正在开发中......");
        });
    }
    void Btn_MiniMode_OnClick()
    {
        //UIMgr.Instance.HidePanel<MenuPanel>();
        UIMgr.Instance.ShowPanel<TipsPanel>(E_UILayer.Top, (panel) =>
        {
            panel.ShowContent("正在开发中......");
        });
    }
    void Btn_SurvivalMode_OnClick()
    {
        //UIMgr.Instance.HidePanel<MenuPanel>();
        UIMgr.Instance.ShowPanel<TipsPanel>(E_UILayer.Top, (panel) =>
        {
            panel.ShowContent("正在开发中......");
        });
    }
    void Btn_ChangeAccount_OnClick()
    {
        //UIMgr.Instance.HidePanel<MenuPanel>();
        UIMgr.Instance.ShowPanel<TipsPanel>(E_UILayer.Top, (panel) =>
        {
            panel.ShowContent("正在开发中......");
        });
    }
    void Btn_IllustratedGuide_OnClick()
    {
        //UIMgr.Instance.HidePanel<MenuPanel>();
        //UIMgr.Instance.ShowPanel<TipsPanel>(E_UILayer.Top, (panel) =>
        //{
        //    panel.ShowContent("正在开发中......");
        //});

        UIMgr.Instance.ShowPanel<IllustratedGuidePanel>();
    }
    void Btn_Achievements_OnClick()
    {
        //UIMgr.Instance.HidePanel<MenuPanel>();
        UIMgr.Instance.ShowPanel<TipsPanel>(E_UILayer.Top, (panel) =>
        {
            panel.ShowContent("正在开发中......");
        });
    }
    void Btn_Shop_OnClick()
    {
        //UIMgr.Instance.HidePanel<MenuPanel>();
        //UIMgr.Instance.ShowPanel<TipsPanel>(E_UILayer.Top, (panel) =>
        //{
        //    panel.ShowContent("正在开发中......");
        //});
        UIMgr.Instance.ShowPanel<ShopPanel>();
    }


    

    protected override void ClickBtn(string name)
    {
        switch (name)
        {
            case "Btn_Quit":
                Btn_Quit_OnClick();
                break;
            case "Btn_Options":
                Btn_Options_OnClick();
                break;
            case "Btn_Help":
                Btn_Help_OnClick();
                break;
            case "Btn_AdventureMode":
                Btn_AdventureMode_OnClick();
                break;
            case "Btn_MiniMode":
                Btn_MiniMode_OnClick();
                break;
            case "Btn_SurvivalMode":
                Btn_SurvivalMode_OnClick();
                break;
            case "Btn_ChangeAccount":
                Btn_ChangeAccount_OnClick();
                break;
            case "Btn_IllustratedGuide":
                Btn_IllustratedGuide_OnClick();
                break;
            case "Btn_Achievements":
                Btn_Achievements_OnClick();
                break;
            case "Btn_Shop":
                Btn_Shop_OnClick();
                break;
        }
    }


    #endregion

}

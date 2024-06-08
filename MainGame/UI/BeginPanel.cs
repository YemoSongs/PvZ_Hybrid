﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始界面
/// </summary>
public class BeginPanel : BasePanel
{
    #region 自定义字段，属性
    private ProgressBar progressBar;

    public float loadingTime = 10f;
    private float time = 0;

    #endregion


    public override void HideMe()
    {
        progressBar.OnProgressEnd -= EndLoading;
    }

    public override void ShowMe()
    {

        progressBar = transform.GetComponentInChildren<ProgressBar>();
        progressBar.OnProgressEnd += EndLoading;
        progressBar.SetProgress(0);

        Loading();
    }


    #region 自定义函数
    void EndLoading()
    {
        progressBar.gameObject.SetActive(false);
        GetControl<Button>("Btn_StartGame").gameObject.SetActive(true);

        UIMgr.Instance.ShowPanel<TipsPanel>(E_UILayer.Top, (panel) =>
        {
            panel.ShowContent("欢迎进入Yemo的PVZ");
        });
    }



    void Loading()
    {
        //用Dotween来计时
        DOVirtual.Float(time, loadingTime, loadingTime, OnValueChanged).onComplete += () =>
        {
            progressBar.SetProgress(1);
        };
    }
    /// <summary>
    /// 在值变化时的回调函数
    /// </summary>
    /// <param name="value"></param>
    void OnValueChanged(float value)
    {
        time = value;
        progressBar.SetProgress(time / loadingTime);
    }

    #endregion

    #region 事件监听

    void Btn_StartGame_Onclick()
    {
        UIMgr.Instance.ShowPanel<MenuPanel>();

        UIMgr.Instance.HidePanel<BeginPanel>(true);
    }



    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_StartGame":
                Btn_StartGame_Onclick();
                break;
        }
    }

    #endregion


   

}

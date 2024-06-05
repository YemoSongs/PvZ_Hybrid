﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : BasePanel
{
    #region 自定义字段，属性

    #endregion    

    public override void HideMe()
    {
        GetControl<TextMeshProUGUI>("Text_Content").text = "";
    }

    public override void ShowMe()
    {


    }

    #region 自定义函数
    public void ShowContent(string content)
    {
        GetControl<TextMeshProUGUI>("Text_Content").text = content;
    }

    #endregion

    #region 事件监听

    void Btn_Confirm_Onclick()
    {
        
        UIMgr.Instance.HidePanel<TipsPanel>();
    }



    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_Confirm":
                Btn_Confirm_Onclick();
                break;
        }
    }

    #endregion

}

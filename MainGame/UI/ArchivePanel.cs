using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchivePanel : BasePanel
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
        //隐藏该存档
        UIMgr.Instance.HidePanel<ArchivePanel>();
    }

    void Btn_Confirm_OnClick()
    {
        //加载选中的存档


        //隐藏该面板
        UIMgr.Instance.HidePanel<ArchivePanel>();
    }

    void Btn_Delete_OnClick()
    {
        //删除选中的存档

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

            case "Btn_Delete":
                Btn_Delete_OnClick();
                break;

            default:
                break;
        }
    }
    void Toggle_Archive_1_ChangeValue(bool value)
    {
        
    }

    void Toggle_Archive_2_ChangeValue(bool value)
    {
        
    }

    void Toggle_Archive_3_ChangeValue(bool value)
    {
        
    }

    void Toggle_Archive_4_ChangeValue(bool value)
    {
        
    }

    void Toggle_Archive_5_ChangeValue(bool value)
    {
        
    }


    protected override void ToggleValueChange(string toggleName, bool value)
    {
        switch (toggleName)
        {
            case "Toggle_Archive_1":
                Toggle_Archive_1_ChangeValue(value);
                break;

            case "Toggle_Archive_2":
                Toggle_Archive_2_ChangeValue(value);
                break;

            case "Toggle_Archive_3":
                Toggle_Archive_3_ChangeValue(value);
                break;

            case "Toggle_Archive_4":
                Toggle_Archive_4_ChangeValue(value);
                break;

            case "Toggle_Archive_5":
                Toggle_Archive_5_ChangeValue(value);
                break;

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
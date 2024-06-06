using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePlantsPanel : BasePanel
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
        UIMgr.Instance.HidePanel<ChoosePlantsPanel>();
        UIMgr.Instance.HidePanel<PlantCardsPanel>();

    }

    void Btn_Start_OnClick()
    {
        //判断选择的植物是否符合要求

        
        UIMgr.Instance.HidePanel<ChoosePlantsPanel>(true);

    }

    void Btn_OpenBook_OnClick()
    {
        UIMgr.Instance.ShowPanel<IllustratedGuidePanel>();
    }

    void Btn_LookLawn_OnClick()
    {
        RectTransform uiPanel = gameObject.GetComponent<RectTransform>();
        // 获取UIPanel的高度
        float panelHeight = uiPanel.rect.height;
        // 计算目标位置
        Vector3 targetPosition = uiPanel.localPosition - new Vector3(0, panelHeight, 0);

        uiPanel.DOLocalMoveY(targetPosition.y, 3).SetEase(Ease.InOutQuad);
        Camera.main.transform.DOMoveX(-15, 5).SetEase(Ease.InOutQuad);
        GetControl<Button>("Btn_OpenThis").gameObject.SetActive(true);
    }


    void Btn_OpenThis_OnClick()
    {
        RectTransform uiPanel = gameObject.GetComponent<RectTransform>();
        // 获取UIPanel的高度
        float panelHeight = uiPanel.rect.height;
        // 计算目标位置
        Vector3 targetPosition = uiPanel.localPosition + new Vector3(0, panelHeight, 0);

        uiPanel.DOLocalMoveY(targetPosition.y, 3).SetEase(Ease.InOutQuad);
        Camera.main.transform.DOMoveX(25, 5).SetEase(Ease.InOutQuad);
        GetControl<Button>("Btn_OpenThis").gameObject.SetActive(false);
    }



    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_Close":
                Btn_Close_OnClick();
                break;

            case "Btn_Start":
                Btn_Start_OnClick();
                break;

            case "Btn_OpenBook":
                Btn_OpenBook_OnClick();
                break;

            case "Btn_LookLawn":
                Btn_LookLawn_OnClick();
                break;

            case "Btn_OpenThis":
                Btn_OpenThis_OnClick();
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
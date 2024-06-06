using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : BasePanel
{
    #region 自定义字段，属性

    [SerializeField] private Transform SV_Content;
    [SerializeField] private ToggleGroup SV_ToggleGroup;


    public List<GameObject> levelList = new List<GameObject>();


    #endregion    

    public override void HideMe()
    {
        
    }

    public override void ShowMe()
    {
        ShowLevels();
    }

    #region 自定义函数

    void ShowLevels()
    {
        for (int i = 0; i < levelList.Count; i++)
        {
            GameObject level = Instantiate(levelList[i],SV_Content);
            level.GetComponent<Toggle>().group = SV_ToggleGroup;
        }
    }


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
        SceneMgr.Instance.LoadSceneAsyn("scene", "GameScene", () =>
        {
            //根据选中的关卡来加载关卡

            if(SV_ToggleGroup != null)
            {
                if (!SV_ToggleGroup.AnyTogglesOn())
                {
                    UIMgr.Instance.ShowPanel<TipsPanel>(E_UILayer.Top, (panel) =>
                    {
                        panel.ShowContent("请选择要进入的关卡！");
                    });
                    return;
                }

                Toggle levelToggle = SV_ToggleGroup.ActiveToggles().FirstOrDefault();

                print("进入关卡" + levelToggle.gameObject.name);
            }
            

            //隐藏当前界面
            UIMgr.Instance.HidePanel<LevelPanel>();

            Camera.main.transform.position = new Vector3(-15, 0, -10);
            Camera.main.transform.DOMoveX(25, 5).OnComplete(() => {
                UIMgr.Instance.ShowPanel<PlantCardsPanel>();
                UIMgr.Instance.ShowPanel<ChoosePlantsPanel>();
            });

        });
        
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
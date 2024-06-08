using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantCardsPanel : BasePanel
{
    #region 自定义字段，属性

    public List<PlantCard> onPlantCards;
    public Transform SV_Content;
    
    public bool isSelecting = false;

    public PlantCard isSelectCard;

    public GameGrid gameGrid;

    //private bool isAddMouseListener = false;

    #endregion    

    public override void HideMe()
    {
        
    }

    public override void ShowMe()
    {
        
    }

    #region 自定义函数

    /// <summary>
    /// 正式开始游戏
    /// </summary>
    public void GameStart()
    {
        if(onPlantCards.Count > 0)
        {
            foreach(PlantCard plantCard in onPlantCards)
            {
                plantCard.isStartGame = true;
            }
        }


        MonoMgr.Instance.AddUpdateListener(MouseListener);
    }

    //鼠标状态监听
    void MouseListener()
    {
        if(isSelecting)
        {
            //选中状态，右键就是取消
            if(Input.GetMouseButtonDown(1))
            {

                isSelecting = false;
                isSelectCard.isSelected = false;
                isSelectCard.SetIsSelected();
                print(isSelectCard.isSelected);
                isSelectCard = null;

            }//选中状态，左键就是种植（种植成功或种植失败都是取消选中）
            else if (Input.GetMouseButtonDown(0))
            {
                print("种植" + isSelectCard.plant.gameObject.name + UtilsClass.GetMouseWorldPosition());

                PlacePlant(UtilsClass.GetMouseWorldPosition());
            }

        }
    }

    /// <summary>
    /// 放置植物
    /// </summary>
    /// <param name="worldPosition"></param>
    public void PlacePlant(Vector3 worldPosition)
    {

        if(isSelecting && isSelectCard!=null)
        {

            if(gameGrid.CanPlacePlant(worldPosition))
            {
                ABResMgr.Instance.LoadResAsync<GameObject>("plant", isSelectCard.plant.plantName, (res) =>
                {
                    GameObject plant = Instantiate(res);

                    gameGrid.PlacePlant(worldPosition, plant);

                    isSelecting = false;
                    isSelectCard.isSelected = false;
                    isSelectCard.SetIsSelected();
                    print(isSelectCard.isSelected);
                    //设置植物种植CD
                    isSelectCard.StartCoolDown();
                    isSelectCard = null;

                    
                });
            }
            else
            {
                print("当前位置不可种植");
                isSelecting = false;
                isSelectCard.isSelected = false;
                isSelectCard.SetIsSelected();
                print(isSelectCard.isSelected);
                isSelectCard = null;

            }
        }        
    }




    #endregion

    #region 控件事件监听
    void Btn_Shovel_OnClick()
    {
        
    }


    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_Shovel":
                Btn_Shovel_OnClick();
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
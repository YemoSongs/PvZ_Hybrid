using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantCardsPanel : BasePanel
{
    #region 自定义字段，属性

    public List<PlantCard> onPlantCards;                //上阵的植物列表
    public Transform SV_Content;                        //植物的父对象

    public bool isGameStart = false;                    //是否是游戏开始状态
    public bool isSelecting = false;                    //是否有植物处在选中状态

    public PlantCard isSelectCard;                      //选中的植物卡片

    public GameGrid gameGrid;                           //游戏网格

    [SerializeField] private RectTransform shovel;      //铲子

    private bool isShovelActive = false;                // 是否处于铲子激活状态
    private Vector3 originalShovelPosition;             // 铲子原始位置

    public TextMeshProUGUI sunNum;    //太阳的数量
 
    #endregion    

    public override void HideMe()
    {
        
    }

    public override void ShowMe()
    {
        originalShovelPosition = shovel.anchoredPosition;
    }

    #region 自定义函数

    /// <summary>
    /// 正式开始游戏
    /// </summary>
    public void GameStart()
    {
        //遍历所有上阵的卡片，设置为游戏开始状态
        if(onPlantCards.Count > 0)
        {
            foreach(PlantCard plantCard in onPlantCards)
            {
                plantCard.isStartGame = true;
            }
        }

        isGameStart = true;

        //添加鼠标左右键监听
        MonoMgr.Instance.AddUpdateListener(MouseListener);



        ABResMgr.Instance.LoadResAsync<LevelData>("leveldata", "LevelData_1", (res) =>
        {
            Level test = new Level(res);
            LevelMgr.Instance.StartLevel(test);
            
        });

       
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


        if (isShovelActive && isGameStart)
        {
            FollowMouse();

            if (Input.GetMouseButtonDown(0))
            {
                ShovelPlant();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                CancelShovel();
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

            if(gameGrid.CanPlacePlant(worldPosition) && JudgeSunNum(isSelectCard.plant.data.cost))
            {
                ABResMgr.Instance.LoadResAsync<GameObject>("plant", isSelectCard.plant.data.plantName, (res) =>
                {
                    GameObject plant = Instantiate(res);

                    gameGrid.PlacePlant(worldPosition, plant);

                    isSelecting = false;
                    isSelectCard.isSelected = false;
                    isSelectCard.SetIsSelected();
                    print(isSelectCard.isSelected);
                    //设置植物种植CD
                    isSelectCard.StartCoolDown();
                    LevelMgr.Instance.ChangeSunNum(-isSelectCard.plant.data.cost);
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



    /// <summary>
    /// 让铲子图片跟随鼠标
    /// </summary>
    void FollowMouse()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)shovel.parent, Input.mousePosition, UIMgr.Instance.uiCamera, out mousePos);
        shovel.anchoredPosition = mousePos;
    }

    /// <summary>
    /// 铲子点击植物对象
    /// </summary>
    void ShovelPlant()
    {
        // 射线检测点击位置

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 发射射线并检测碰撞
        if (Physics.Raycast(ray, out hit))
        {
            // 获取射线碰撞到的对象的信息
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("Hit object: " + hitObject.name);

            if (hit.collider != null)
            {
                Plant plant = hit.collider.GetComponent<Plant>();
                if (plant != null)
                {
                    plant.BeShoveled();
                    CancelShovel();
                }
            }
        }
       
        else
        {
            CancelShovel();
        }
    }

    // 取消铲子操作
    void CancelShovel()
    {
        isShovelActive = false;
        shovel.anchoredPosition = originalShovelPosition;
        //shovel.gameObject.SetActive(false);
    }



    bool JudgeSunNum(int plantcost)
    {
        if( LevelMgr.Instance.nowSunNum - plantcost >= 0)
            return true;
        return false;
    }



    #endregion

    #region 控件事件监听


    //铲子按钮
    void Btn_Shovel_OnClick()
    {
        isShovelActive = true;
        //shovel.gameObject.SetActive(true);
        shovel.SetAsLastSibling(); // 将铲子图片置于最前
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
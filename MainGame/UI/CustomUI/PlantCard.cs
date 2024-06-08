using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// 植物卡片类 
/// </summary>
public class PlantCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    #region 属性，字段
    //通用字段
    [SerializeField] private Button btn_Plant;                  //卡片按钮（植物卡片是一个按钮）
    [SerializeField] private Image img_Card;                    //卡片展示的图片
    [SerializeField] private Image img_CD;                      //卡片CD或选中状态的遮罩图片


    private Sprite card;                                        //在选卡片的时候没选中时的图片
    private Sprite onCard;                                      //在选卡片的时候选中时的图片

    [SerializeField] private bool isOnCardsPanel = false;       //是否在选中框里面
    public bool isStartGame = false;                            //是否已经开始游戏
    public bool isSelected = false;                             //是否是选中状态
    public bool isCoolDown = false;                             //是否处在CD中
    
    GameObject rootPlantCard;                                   //在选择植物界面的母体对象


    public PlantCardsPanel plantCardsPanel;                     //上阵植物界面
    public ChoosePlantsPanel choosePlantsPanel;                 //选择植物界面
   
    public Plant plant;                                         //该卡片表示的植物的字段

    #endregion


    private void Start()
    {
        card = plant.cardImg;
        onCard = plant.onCardImg;
        img_Card.sprite = card;
        img_CD.gameObject.SetActive(false);

        UIMgr.Instance.GetPanel<PlantCardsPanel>((panel) => { plantCardsPanel = panel; });

        UIMgr.Instance.GetPanel<ChoosePlantsPanel>((panel) => { choosePlantsPanel = panel; });

        btn_Plant.onClick.AddListener(() =>
        {
            //在选植物面板时，如果不在上阵的植物面板中，就放进去
            if (!isOnCardsPanel)
            {

                GameObject card = Instantiate(gameObject,plantCardsPanel.SV_Content);
                card.GetComponent<RectTransform>().sizeDelta = new Vector2(105, 148);
                PlantCard plantCard = card.GetComponent<PlantCard>();
                plantCard.rootPlantCard = gameObject;
                plantCard.isOnCardsPanel = true;
                plantCardsPanel.onPlantCards.Add(plantCard);

            }
            //如果在上阵植物面板中，并且自己在选植物面板的母体不为空，并且不是处在开始游戏状态下，就从上阵植物面板上下来
            else if(isOnCardsPanel && rootPlantCard != null && !isStartGame)
            {
                rootPlantCard.GetComponent<PlantCard>().isOnCardsPanel = false;
                rootPlantCard.GetComponent<PlantCard>().img_Card.sprite = card;
                plantCardsPanel.onPlantCards.Remove(this);
                Destroy(gameObject);
            }

            ////如果已经开始游戏 并且 上阵的植物面板中没有选中的植物 ，就把自己设置为选中状态，并把自己传给上阵植物面板
            //if (isStartGame && !plantCardsPanel.isSelecting)
            //{
            //    isSelected = true;
            //    plantCardsPanel.isSelectCard = this;
            //    return;
            //}

            if (isStartGame)
                return;
           
            //如果不在开始游戏状态时，就会设为 在上阵植物界面 并更改自己的图片
            isOnCardsPanel = true;
            img_Card.sprite = onCard;
        });
    }


    #region 函数

    /// <summary>
    /// 设置植物卡片当前的选中效果
    /// </summary>
    public void SetIsSelected()
    {
        if(isSelected)
            img_CD.gameObject.SetActive(true);
        else
            img_CD.gameObject.SetActive(false);
    }

    public void StartCoolDown()
    {
        if(!isCoolDown)
        {
            isCoolDown = true;
            img_CD.gameObject.SetActive(true); // 确保冷却遮罩启用

            DOVirtual.Float(1, 0, plant.coolDown, (value) =>
            {
                img_CD.fillAmount = value;
            }).SetEase(Ease.Linear).onComplete += () =>
            {
                isCoolDown = false;
                img_CD.gameObject.SetActive(false); // 冷却结束后隐藏遮罩
                img_CD.fillAmount = 1;
            };
        }
    }



    // 实现IPointerDownHandler接口的方法
    public void OnPointerDown(PointerEventData eventData)
    {
        //如果游戏还没开始 或者 上阵植物面板已经在选中状态 就忽略按下卡片的操作
        if (!isStartGame || plantCardsPanel.isSelecting || isCoolDown)
            return;

        //按下植物卡片 设为选中状态 并把自己这个植物卡片传给上阵植物列表
        //不能在这里就立即设置设置上阵植物列表的选中状态， 要等到抬起鼠标时，才能设置上阵植物列表的选中状态
        //不然的话，会在重新选中植物卡片时，直接触发种植的逻辑（同时是按下鼠标左键&&处于选中状态）
        isSelected = true;
        plantCardsPanel.isSelectCard = this;
        SetIsSelected();
        print(isSelected);

    }

    // 实现IPointerUpHandler接口的方法
    public void OnPointerUp(PointerEventData eventData)
    {
        //当抬起鼠标时 
        if(isSelected && !plantCardsPanel.isSelecting)
        {
            plantCardsPanel.isSelecting = isSelected;
        }
        
    }

    #endregion
}

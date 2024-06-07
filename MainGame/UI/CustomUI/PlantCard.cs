using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //通用字段
    [SerializeField] private Button btn_Plant;
    [SerializeField] private Image img_Card;
    private Sprite card;
    private Sprite onCard;
    [SerializeField] private bool isOnCardsPanel = false;//是否在选中框里面
    public bool isStartGame = false;//是否已经开始游戏
    public bool isSelected = false;//是否是选中状态

    //在选择植物界面的对象
    GameObject rootPlantCard;

    [HideInInspector]public GameGrid gameGrid;



    public PlantCardsPanel plantCardsPanel;
    public ChoosePlantsPanel choosePlantsPanel;


    //该卡片表示的植物的字段
    public Plant plant;


    private void Start()
    {
        card = plant.cardImg;
        onCard = plant.onCardImg;
        img_Card.sprite = card;

        UIMgr.Instance.GetPanel<PlantCardsPanel>((panel) => { plantCardsPanel = panel; });

        UIMgr.Instance.GetPanel<ChoosePlantsPanel>((panel) => { choosePlantsPanel = panel; });

        btn_Plant.onClick.AddListener(() =>
        {

            if (!isOnCardsPanel)
            {

                GameObject card = Instantiate(gameObject,plantCardsPanel.SV_Content);
                card.GetComponent<RectTransform>().sizeDelta = new Vector2(105, 148);
                PlantCard plantCard = card.GetComponent<PlantCard>();
                plantCard.rootPlantCard = gameObject;
                plantCard.isOnCardsPanel = true;
                plantCardsPanel.onPlantCards.Add(plantCard);

            }
            else if(isOnCardsPanel && rootPlantCard != null && !isStartGame)
            {
                rootPlantCard.GetComponent<PlantCard>().isOnCardsPanel = false;
                rootPlantCard.GetComponent<PlantCard>().img_Card.sprite = card;
                plantCardsPanel.onPlantCards.Remove(this);
                Destroy(gameObject);
            }

            if (isStartGame && !plantCardsPanel.isSelecting)
            {
                isSelected = true;
                plantCardsPanel.isSelectCard = this;
                return;
            }

            if (isStartGame)
                return;
           

            isOnCardsPanel = true;
            img_Card.sprite = onCard;
        });
    }


    // 实现IPointerDownHandler接口的方法
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isStartGame || plantCardsPanel.isSelecting)
            return;

        isSelected = true;
        plantCardsPanel.isSelectCard = this;
       
        print(isSelected);

    }

    // 实现IPointerUpHandler接口的方法
    public void OnPointerUp(PointerEventData eventData)
    {
        if(isSelected && !plantCardsPanel.isSelecting)
        {
            plantCardsPanel.isSelecting = isSelected;
        }
        
    }



    /// <summary>
    /// 放置植物
    /// </summary>
    /// <param name="worldPosition"></param>
    public void PlacePlant(Vector3 worldPosition)
    {

        ABResMgr.Instance.LoadResAsync<GameObject>("plant", "Peashooter", (res) =>
        {
            gameGrid.PlacePlant(worldPosition, res);
        });
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 植物数据类
/// </summary>
[CreateAssetMenu(fileName = "PlantData", menuName = "ScriptableObjects/PlantData", order = 1)]
public class PlantData : ScriptableObject
{
    public Sprite cardImg;          //正常情况的植物卡片图片
    public Sprite onCardImg;        //上阵后的植物卡片图片

    public string plantName;        //植物名字
    public int maxHealth;           //植物的生命值
    public int cost;                //植物的种植费用
    public float coolDown;          //种植后的冷却时间               
    public float attackInterval;    //攻击间隔时间
    
    public LayerMask enemyLayer;    //检测敌人层级
    public BulletData bulletData;   //子弹的数据
}

using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "ScriptableObjects/ZombieData", order = 1)]
public class ZombieData : ScriptableObject
{
    public string zombieName;       //僵尸名字
    public float maxHealth;            //僵尸血量
    public float moveSpeed;         //移动速度
    public int damage;              //攻击伤害
    public float attackInterval;    //攻击间隔时间
    public LayerMask enemyLayer;    //检测敌人层级
}

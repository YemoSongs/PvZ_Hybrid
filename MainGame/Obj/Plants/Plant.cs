using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    public Sprite cardImg;          //正常情况的植物卡片图片
    public Sprite onCardImg;        //上阵后的植物卡片图片


    public GridCell gridCell;       //自己对于的网格单元

    public string plantName;        //植物名字
    public int health;              //植物的生命值
    public int cost;                //植物的种植费用
    public float coolDown;          //种植后的冷却时间               


    public bool canAtk = false;     //是否可以攻击


    public float attackInterval;    //攻击间隔时间
    
    protected int attackTimer;      //攻击间隔计时器



    protected virtual void Start()
    {
        PlantTimer();
    }

    /// <summary>
    /// 用于执行攻击行为的计时器
    /// </summary>
    protected virtual void PlantTimer()
    {

        attackTimer = TimerMgr.Instance.CreateTimer(false, (int)(attackInterval * 1000), () =>
        {
            if (canAtk)
            {
                Attack();
            }
        });

        TimerMgr.Instance.StopTimer(attackTimer);
    }


    // 攻击方法，由子类实现具体攻击行为
    protected abstract void Attack();

    // 受伤方法，当植物受到伤害时调用
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // 植物死亡时的处理方法
    protected virtual void Die()
    {
        TimerMgr.Instance.RemoveTimer(attackTimer); 

        Destroy(gameObject); // 销毁植物对象
    }
}

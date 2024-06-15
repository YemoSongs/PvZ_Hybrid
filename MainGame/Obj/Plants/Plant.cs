using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    public PlantData data;             //植物数据

    public SpriteRenderer spriteRenderer;   //植物图片渲染器


    [SerializeField]
    protected float currentHealth;          //植物当前的血量

    public bool canAtk = false;             //是否可以攻击

    public GridCell gridCell;               //自己对于的网格单元

    protected int attackTimer;              //攻击间隔计时器




    private Animator animator;
    [SerializeField] private string currentAnimation = "";


    protected virtual void ChangeAnimation(string animation, float crossfade = 0.2f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crossfade);
        }
    }




    protected virtual void Start()
    {
        PlantTimer();

        currentHealth = data.maxHealth;
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// 用于执行攻击行为的计时器
    /// </summary>
    protected virtual void PlantTimer()
    {

        attackTimer = TimerMgr.Instance.CreateTimer(false, (int)(data.attackInterval * 1000), () =>
        {
            if (canAtk)
            {
                Attack();
            }
        });

        TimerMgr.Instance.StopTimer(attackTimer);
    }


    /// <summary>
    /// 攻击方法，由子类实现具体攻击行为
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// 受伤方法，当植物受到伤害时调用
    /// </summary>
    /// <param name="damage">受到多少伤害</param>
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    /// <summary>
    /// 被铲子铲的方法
    /// </summary>
    public virtual void BeShoveled()
    {
        gridCell.RemoveObject();

        Die();
    }



    /// <summary>
    /// 植物死亡时的处理方法
    /// </summary>
    protected virtual void Die()
    {
        TimerMgr.Instance.RemoveTimer(attackTimer); 

        Destroy(gameObject); // 销毁植物对象
    }
}

using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    public Sprite cardImg;
    public Sprite onCardImg;



    public int health;           // 植物的生命值
    public int cost;             // 植物的种植费用
    public float attackInterval; // 攻击间隔时间

    private float attackTimer;   // 计时器


    protected virtual void Start()
    {
        attackTimer = 0f;
    }

    protected virtual void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            Attack();
            attackTimer = 0f;
        }
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
        Destroy(gameObject); // 销毁植物对象
    }
}

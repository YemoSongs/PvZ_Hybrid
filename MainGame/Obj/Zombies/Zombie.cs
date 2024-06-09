using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zombie : MonoBehaviour
{
    public ZombieData data;

    [SerializeField]
    protected float currentHealth;            //僵尸当前血量

    public SpriteRenderer spriteRenderer;   //僵尸图片渲染器

    public bool canAtk = false;             //是否可以攻击
    public bool canMove = false;            //是否可以攻击
    public float detectionInterval = 0.5f;  // 检测间隔时间



    public Vector3 attackCenter = Vector3.zero;
    public Vector3 attackRange = Vector3.one;

    protected virtual void Start()
    {


        currentHealth = data.maxHealth;

        // 启动协程来执行检测
        StartCoroutine(DetectObstacle());
    }


    protected virtual void Update()
    {
        Move();
    }


    protected virtual void Move()
    {
        if (canMove)
        {
            transform.Translate(Vector3.left * Time.deltaTime * data.moveSpeed*0.1f);
        }
    }


    /// <summary>
    /// 检测前方是否有植物的协程
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator DetectObstacle()
    {
        while (true)
        {

            // 创建一个位于僵尸前方的小触发器
            Collider[] colliders = Physics.OverlapBox(transform.position + attackCenter, attackRange, Quaternion.identity,data.enemyLayer);

            if (colliders.Length > 0)
            {
                // 如果发生碰撞，则表示前方有障碍物,停止移动
                canMove = false;
                canAtk = true;
                Attack(colliders[0].GetComponent<Plant>());
                yield return new WaitForSeconds(data.attackInterval);
            }
            else
            {
                canMove = true;
            }
            //每隔detectionInterval时间检测一次前方是否可以行走
            yield return new WaitForSeconds(detectionInterval);
        }
    }


    /// <summary>
    /// 攻击方法，由子类实现具体攻击行为
    /// </summary>
    protected abstract void Attack(Plant plant);


    /// <summary>
    /// 受伤方法，当僵尸受到伤害时调用
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
    /// 僵尸死亡时的处理方法
    /// </summary>
    protected virtual void Die()
    {
        Destroy(gameObject); // 销毁植物对象
    }



    private void OnDrawGizmosSelected()
    {
        // 绘制攻击范围的长方体区域
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + attackCenter, attackRange);
    }
}

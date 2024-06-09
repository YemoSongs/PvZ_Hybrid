using UnityEngine;

public class Peashooter : Plant
{

    public Transform shootPoint; // 发射点

    private float attackRange = 5f; // 攻击范围的长度
    private float attackWidth = 5f; // 攻击范围的宽度


    protected override void Start()
    {
        base.Start();
        SetATKRange();
        canAtk = true; // 豌豆射手可以攻击
        TimerMgr.Instance.StartTimer(attackTimer); // 开始攻击计时器
    }

    //设置攻击范围
    void SetATKRange()
    {
        Vector2 pos = gridCell.pos;
        attackWidth = (gridCell.gameGrid.width - pos.x)* gridCell.gameGrid.cellSize;
        attackRange = gridCell.gameGrid.cellSize;
    }


    protected override void Attack()
    {
        if (DetectEnemy())
        {
            // 实例化一个豌豆并发射
            GameObject pea = PoolMgr.Instance.GetObj("Bullet/Pea");
            pea.transform.SetParent(shootPoint, false);
            pea.transform.localPosition = Vector3.zero;
            pea.transform.localRotation = Quaternion.identity;
            // 设置豌豆的具体行为，比如设置速度等
            pea.GetComponent<Pea>().Init(data.bulletData);
        }
    }

    private bool DetectEnemy()
    {
        // 定义长方体的中心和半尺寸
        Vector3 center = transform.position + transform.right * (attackWidth / 2);
        Vector3 halfExtents = new Vector3(attackWidth / 2, attackRange / 2, 1 );

        // 检测前方的敌人
        Collider[] enemies = Physics.OverlapBox(center, halfExtents, transform.rotation, data.enemyLayer);
        return enemies.Length > 0;
    }

    private void OnDrawGizmosSelected()
    {
        // 绘制攻击范围的长方体区域
        Gizmos.color = Color.red;
        Vector3 center = transform.position + transform.right * (attackWidth / 2);
        Vector3 halfExtents = new Vector3(attackWidth / 2, attackRange / 2, 1 );
        Gizmos.DrawWireCube(center, halfExtents * 2);
    }
}

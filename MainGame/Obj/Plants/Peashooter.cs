using UnityEngine;

public class Peashooter : Plant
{
    public GameObject peaPrefab; // 豌豆的预制体
    public Transform shootPoint; // 发射点



    protected override void Attack()
    {
        // 实例化一个豌豆并发射

        GameObject pea = PoolMgr.Instance.GetObj("Bullet/Pea");
        pea.transform.SetParent(shootPoint, false);
        pea.transform.localPosition = Vector3.zero;
        pea.transform.localRotation = Quaternion.identity;
        // 这里可以添加豌豆的具体行为，比如设置速度等
        pea.GetComponent<Pea>().Init(10, 20);

    }
}

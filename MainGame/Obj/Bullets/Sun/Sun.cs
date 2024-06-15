using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{


    public int sunNum;

    public float sunDelayTime;
    public float fallSpeed = 1f; // 下落速度
    public bool isFalling = true; // 标识阳光是否需要下落
    public float targetYPosition = 0f; // 阳光下落的目标Y位置

    void Start()
    {
        Destroy(gameObject,sunDelayTime);
    }
  
    private void Update()
    {
        if (isFalling)
        {
            // 阳光下落
            transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);


            // 检查是否到达目标位置
            if (transform.position.y <= targetYPosition)
            {
                // 停止下落
                isFalling = false;
                // 修正位置，防止穿过地面
                transform.position = new Vector3(transform.position.x, targetYPosition, transform.position.z);
            }
        }
    }


    private void OnMouseDown()
    {
        // 玩家点击阳光时收集阳光
        CollectSun();
    }

    private void CollectSun()
    {
        // 增加阳光计数
        LevelMgr.Instance.ChangeSunNum(sunNum); // 每个阳光增加sunNum阳光值
        Destroy(gameObject); // 销毁阳光对象
    }


}

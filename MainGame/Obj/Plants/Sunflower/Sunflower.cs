using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : Plant
{


    protected override void Start()
    {
        base.Start();
        canAtk = true; 
        TimerMgr.Instance.StartTimer(attackTimer); // 开始攻击计时器
        ChangeAnimation("Sunflower_Idle");
    }


    protected override void Attack()
    {
        ChangeAnimation("Sunflower_Action");

        ABResMgr.Instance.LoadResAsync<GameObject>("bullet", "Sun", (res) =>
        {
            GameObject sun = Instantiate(res, gameObject.transform.position, Quaternion.identity);
            sun.GetComponent<Sun>().isFalling = false;
        });
        
    }




}

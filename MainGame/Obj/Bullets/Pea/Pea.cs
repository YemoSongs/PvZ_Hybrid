using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Pea : MonoBehaviour
{
    private BulletData bulletData;


    public void Init(BulletData bulletData)
    {
        this.bulletData = bulletData;

        
        gameObject.GetComponent<SpriteRenderer>().sprite = bulletData.bulletSprite;
    }

    private void OnEnable()
    {
        Invoke("DelayPush", 10);
    }



    void DelayPush()
    {
        PoolMgr.Instance.PushObj(gameObject);
    }


    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bulletData.speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (MathUtil.IsLayerInLayerMask(collision.gameObject.layer, bulletData.atkLayer))
        {
            Zombie zombie = collision.gameObject.GetComponent<Zombie>();
            zombie.TakeDamage(bulletData.damage);
            print($"{bulletData.name}打到了{zombie.data.name}，造成了{bulletData.damage}点伤害");

            CancelInvoke();
            DelayPush();
        }
    }


}

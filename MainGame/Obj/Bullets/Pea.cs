using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pea : MonoBehaviour
{

    private float speed = 10;
    private float atkValue = 20;

    public void Init(float speed,float atk)
    {
        this.speed = speed;
        this.atkValue = atk;
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
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    
}

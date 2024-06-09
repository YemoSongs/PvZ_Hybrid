using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData", order = 1)]
public class BulletData : ScriptableObject
{
    public string bulletName;
    public float damage;
    public float speed;
    public Sprite bulletSprite;
    public LayerMask atkLayer;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalZombie : Zombie
{

    protected override void Start()
    {
        base.Start();


        ChangeAnimation("NomalZombie_Move");
    }




    protected override void Attack(Plant plant)
    {
        if (plant == null)
        {
            ChangeAnimation("NomalZombie_Move");
            return;
        }
            
        plant.TakeDamage(data.damage);
        print($"{data.name}对{plant.data.name}造成了{data.damage}伤害");
        ChangeAnimation("NomalZombie_Eat");
    }

    protected override void Die()
    {
        base.Die();
        ChangeAnimation("NomalZombie_Die");

        Destroy(gameObject,2);
    }


}

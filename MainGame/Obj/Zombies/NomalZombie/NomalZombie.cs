using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalZombie : Zombie
{

    protected override void Attack(Plant plant)
    {
        plant.TakeDamage(data.damage);
        print($"{data.name}对{plant.data.name}造成了{data.damage}伤害");
    }
}

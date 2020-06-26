using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}

public interface ICurable
{
    void Heal(int amount);
}

public interface IPickupable
{
    void Pickup();
}

public interface IAttack
{
    void DoAttack();
}
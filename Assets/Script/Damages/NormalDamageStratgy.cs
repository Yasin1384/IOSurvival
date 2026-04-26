using UnityEngine;

public class NormalDamageStratgy : IDamageStratgy
{
    public int Damage(int damage)
    {
        return damage;
    }

    public int EnemyDamage(int damage)
    {
        return damage;
    }
}

using UnityEngine;

public interface IDamageStratgy
{
    int Damage(int damage);
    int EnemyDamage(int damage);
    int BaseDamage(int damage);
}

using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    
    private IDamageStratgy _damageStratgy;
    private EnemyType_SO EnemyType_SO;
    private int currentHp;

    public void Init(EnemyType_SO data)
    {
        EnemyType_SO = data;
        
        currentHp = data.Hp;

        _damageStratgy = new NormalDamageStratgy();
    }

    public void TakeDamage(int baseDamage)
    {
        if (_damageStratgy == null) return;

        currentHp -= baseDamage;

        if (currentHp <= 0)
        {
            GetComponent<MovementEnemy>().Die();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        MovementEnemy enemy = GetComponent<MovementEnemy>();

        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);

            if (EnemyType_SO.Hp <= 0)
            {
                enemy.Die();
            }
        }
        else
        {
            TakeDamage(EnemyType_SO.Hp);

            if (EnemyType_SO.Hp <= 0)
            {
                enemy.Die();
            }
        }
    }
}

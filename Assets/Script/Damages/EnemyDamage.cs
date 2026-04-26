using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    private IDamageStratgy _damageStratgy;
    public EnemyType_SO EnemyType_SO;

    private void Start()
    {
        SetDamage(new NormalDamageStratgy());
    }
    private void SetDamage(IDamageStratgy damageStratgy)
    {
        _damageStratgy = new NormalDamageStratgy();
    }

    public void TakeDamage(int baseDamage)
    {
        if (_damageStratgy == null)
        {
            Debug.Log(EnemyType_SO.EnemyDamage);

            return;
        }

        int finalDamage = _damageStratgy.EnemyDamage(baseDamage);
        EnemyType_SO.EnemyDamage -= finalDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        MovementEnemy enemy = GetComponent<MovementEnemy>();

        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);

            if (EnemyType_SO.EnemyDamage <= 0)
            {
                enemy.Die();
            }
        }
        else
        {
            TakeDamage(EnemyType_SO.EnemyDamage);

            if (EnemyType_SO.EnemyDamage <= 0)
            {
                enemy.Die();
            }
        }
    }
}

using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    private IDamageStratgy _damageStratgy;
    private int _enemyDamage = 20;
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
            Debug.Log(_enemyDamage);

            return;
        }

        int finalDamage = _damageStratgy.EnemyDamage(baseDamage);
        _enemyDamage -= finalDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        MovementEnemy enemy = GetComponent<MovementEnemy>();

        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);

            if (_enemyDamage <= 0)
            {
                enemy.Die();
            }
        }
        else
        {
            TakeDamage(_enemyDamage);

            if (_enemyDamage <= 0)
            {
                enemy.Die();
            }
        }
    }
}

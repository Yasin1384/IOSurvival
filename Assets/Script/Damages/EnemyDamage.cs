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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
        else if(collision.gameObject.CompareTag("Base") || collision.gameObject.CompareTag("Player"))
        {
            TakeDamage(currentHp);
        }
    }
}

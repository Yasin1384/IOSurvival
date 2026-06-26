using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private IDamageStratgy _damageStratgy;
    private EnemyType_SO EnemyType_SO;
    private int currentHp;
    [SerializeField] private ParticleSystem _particleSystem;

    public void Init(EnemyType_SO data)
    {
        float enemyHp = EnemyManager.Instance.CurrentHp;

        EnemyType_SO = data;
        
        currentHp = data.Hp;

        enemyHp = currentHp;

        _damageStratgy = new NormalDamageStratgy();
    }



    public void TakeDamage(int baseDamage)
    {
        if (_damageStratgy == null) return;

        currentHp -= baseDamage;

        if (currentHp <= 0)
        {
            if (!GetComponent<MovementEnemy>())
            {
                GetComponent<ContorlEnemy>().Die();
            }
            else
            {
                GetComponent<MovementEnemy>().Die();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
            _particleSystem.Play();
        }
        else if (other.gameObject.CompareTag("Base") || other.gameObject.CompareTag("Player"))
        {
            TakeDamage(currentHp);
        }
    }
}

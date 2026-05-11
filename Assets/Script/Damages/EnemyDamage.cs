using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private IDamageStratgy _damageStratgy;
    private EnemyType_SO EnemyType_SO;
    private int currentHp;

    private int killBonus;
    private void Awake()
    {
        EnemyManager.Instance.SaveDamageEnemy();
    }

    public void Init(EnemyType_SO data)
    {
        float enemyHp = EnemyManager.Instance.CurrentHp;

        EnemyType_SO = data;
        
        currentHp = data.Hp;

        EnemyManager.Instance.LoadDamageEnemy();

        enemyHp = currentHp;

        _damageStratgy = new NormalDamageStratgy();
    }

    private void AddXp(EnemyType_SO data)
    {
        killBonus += data.KillBonus;
    }

    public void TakeDamage(int baseDamage)
    {
        if (_damageStratgy == null) return;

        currentHp -= baseDamage;

        if (currentHp <= 0)
        {
            AddXp(EnemyType_SO);
            GetComponent<MovementEnemy>().Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
        else if (other.gameObject.CompareTag("Base") || other.gameObject.CompareTag("Player"))
        {
            TakeDamage(currentHp);
        }
    }
}

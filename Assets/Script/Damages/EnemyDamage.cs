using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private IDamageStratgy _damageStratgy;
    private EnemyType_SO EnemyType_SO;
    private int currentHp;
    private void Awake()
    {
        EnemyManager.Instance.SaveDamageEnemy();
    }

    public void Init(EnemyType_SO data)
    {
        EnemyManager.Instance.LoadDamageEnemy();
        currentHp = EnemyManager.Instance.CurrentHp;

        EnemyType_SO = data;
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

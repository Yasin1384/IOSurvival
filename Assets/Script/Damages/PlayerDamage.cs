using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private IDamageStratgy _damageStratgy;
    private int _damage = 30;
    private void Start()
    {
        SetDamage(new NormalDamageStratgy());
    }
    private void SetDamage(IDamageStratgy damageStratgy)
    {
        _damageStratgy = new NormalDamageStratgy();
        Debug.Log(_damage);
    }

    public void TakeDamage(int baseDamage)
    {
        if (_damageStratgy == null)
        {
            Debug.Log(_damage);

            return;
        }

        int finalDamage = _damageStratgy.Damage(baseDamage);
        _damage -= finalDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Desttoy");
            TakeDamage(10);

            if (_damage <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    private IDamageStratgy _damageStratgy;
    private int _baseDamage = 100;
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
            Debug.Log(_baseDamage);

            return;
        }

        int finalDamage = _damageStratgy.BaseDamage(baseDamage);
        _baseDamage -= finalDamage;



        if (_baseDamage <= 0)
        {
            GameManager.Instance.GameOver(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }
}

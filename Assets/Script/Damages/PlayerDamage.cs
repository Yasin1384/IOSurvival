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
    }

    public void TakeDamage(int baseDamage)
    {
        if (_damageStratgy == null)
        {
            return;
        }

        int finalDamage = _damageStratgy.Damage(baseDamage);
        _damage -= finalDamage;


        if (_damage <= 0)
        {
            //TODO : panel Game Over 
            GameManager.Instance.GameOver(gameObject);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);

        }
    }

}

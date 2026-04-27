using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        EnemyDamage enemy = collision.gameObject.GetComponent<EnemyDamage>();

        if (enemy != null)
        {
            enemy.TakeDamage(10);
        }

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

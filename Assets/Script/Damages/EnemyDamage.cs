using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        MovementEnemy enemy = GetComponent<MovementEnemy>();

        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.Die();
        }
    }
}

using UnityEngine;

public class BulletTowerCannon : MonoBehaviour
{
    private BulletTowerCannonPool poolCannon;

    [Header("Explosion")]
    private bool explodeOnHit = true;
    [SerializeField] private float explosionRadius = 500f;
    [SerializeField] private int explosionDamage = 2050;
    [SerializeField] private LayerMask enemyLayer;

    public void SetPoolCannon(BulletTowerCannonPool p)
    {
        poolCannon = p;
    }

    private void OnTriggerEnter(Collider other)
    {
        poolCannon.Despawn(gameObject);
        Explode();
    }

    private void Explode()
    {
        Debug.Log($"Explode at {transform.position}, radius={explosionRadius}");

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
        Debug.Log($"Hit count = {hitEnemies.Length}");

        Debug.Log("Attack");
        Debug.Log(hitEnemies);

        foreach (var col in hitEnemies)
        {
            var enemyHealth = col.GetComponent<EnemyDamage>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(explosionDamage);
            }
        }
    }

    private void DespawnBullet()
    {
        if (poolCannon != null)
            poolCannon.Despawn(gameObject);
        else
            Destroy(gameObject);
    }
}

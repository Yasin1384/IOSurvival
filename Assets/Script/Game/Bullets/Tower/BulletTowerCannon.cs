using UnityEngine;

public class BulletTowerCannon : MonoBehaviour
{
    private BulletTowerPool poolCannon;

    [Header("Explosion")]
    private bool explodeOnHit = true;
    [SerializeField] private float explosionRadius = 500f;
    [SerializeField] private int explosionDamage = 2050;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private ParticleSystem particleSystemGameObject;

    public void SetPoolCannon(BulletTowerPool p)
    {
        poolCannon = p;
    }

    private void OnTriggerEnter(Collider other)
    {
        poolCannon.Despawn(gameObject);
        Vector3 deathPosition = new Vector3(transform.position.x, 1, transform.position.z);
        Instantiate(particleSystemGameObject.gameObject, deathPosition, Quaternion.identity);
        particleSystemGameObject.Play();
        Explode();
    }

    private void Explode()
    {

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);

        foreach (var col in hitEnemies)
        {
            var enemyHealth = col.GetComponent<EnemyDamage>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(explosionDamage);
            }
        }
    }
}

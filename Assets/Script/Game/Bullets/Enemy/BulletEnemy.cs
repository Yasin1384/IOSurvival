using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private BulletEnemyPool pool;

    public void SetPool(BulletEnemyPool p)
    {
        pool = p;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (pool != null)
    //        {
    //            pool.Despawn(gameObject);
    //        }
    //    }

    //    pool.Despawn(gameObject);
    //}

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}

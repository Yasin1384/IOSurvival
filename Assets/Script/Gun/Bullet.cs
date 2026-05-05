using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletPool pool;

    public void SetPool(BulletPool p)
    {
        pool = p;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pool != null)
        {
            pool.Despawn(gameObject);
        }
    }
}


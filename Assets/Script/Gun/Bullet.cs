using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;
    private BulletPool pool;
    private bool hasCollided = false;
    private Coroutine lifeTimerCoroutine;

    public void Init(BulletPool poolReference)
    {
        pool = poolReference;
    }

    private void OnEnable()
    {
        hasCollided = false;

        if (lifeTimerCoroutine != null)
        {
            StopCoroutine(lifeTimerCoroutine);
        }
        lifeTimerCoroutine = StartCoroutine(DestroyAfterTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return;

        hasCollided = true;

        if (lifeTimerCoroutine != null)
        {
            StopCoroutine(lifeTimerCoroutine);
        }


        if (pool != null)
        {
            pool.Despawn(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);

        if (!hasCollided)
        {
            if (pool != null)
            {
                pool.Despawn(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

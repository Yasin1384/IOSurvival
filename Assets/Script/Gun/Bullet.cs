using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;
    private bool hasCollided = false;
    private Coroutine lifeTimerCoroutine;

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
        BulletPool.Instance.Despawn(gameObject);

    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        this.gameObject.SetActive(false);
    }
}

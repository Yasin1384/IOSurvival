using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        BulletPool.Instance.Despawn(gameObject);
    }
}


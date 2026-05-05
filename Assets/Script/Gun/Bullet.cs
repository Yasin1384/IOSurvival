using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{   private void OnTriggerEnter(Collider other)
    {
        BulletPool.Instance.Despawn(gameObject);

    }
}


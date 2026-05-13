using System.Collections.Generic;
using UnityEngine;

public class BulletTowerPool : MonoBehaviour
{
    public GameObject BulletTowerPrefab;
    public int poolSize = 10;

    private Queue<GameObject> pool;

    public void Initialize(GameObject BulletTowerPrefab, int poolSize)
    {
        this.BulletTowerPrefab = BulletTowerPrefab;
        this.poolSize = poolSize;

        pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(this.BulletTowerPrefab);
            obj.SetActive(false);

            pool.Enqueue(obj);
        }
    }
    public GameObject Spawn(Vector3 position)
    {
        GameObject obj = pool.Dequeue();

        obj.transform.position = position;
        obj.transform.rotation = Quaternion.identity;

        obj.SetActive(true);

        pool.Enqueue(obj);

        return obj;
    }

    public void Despawn(GameObject bullet)
    {
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        bullet.SetActive(false);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject BulletPrefab;
    public int poolSize = 10;

    private List<GameObject> pool;

    public void Initialize(GameObject bulletPrefab, int poolSize)
    {
        this.BulletPrefab = bulletPrefab;
        this.poolSize = poolSize;

        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(this.BulletPrefab);
            obj.GetComponent<Bullet>().Init(this);

            obj.SetActive(false);
            pool.Add(obj);
        }
    }
    public GameObject Spawn(Vector3 position)
    {
        foreach (var bullet in pool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = position;
                bullet.SetActive(true);
                return bullet;
            }
        }

        return null;
    }

    public void Despawn(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}

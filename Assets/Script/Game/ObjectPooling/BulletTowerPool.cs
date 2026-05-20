using System.Collections.Generic;
using UnityEngine;

public class BulletTowerPool : MonoBehaviour
{
    public SpawnBulletData bulletPoolConfigs;
    private Dictionary<string, SpawnBulletData> poolConfigDictionary;
    private Dictionary<GameObject, string> objectToBulletTypeName;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        poolConfigDictionary = new Dictionary<string, SpawnBulletData>();
        objectToBulletTypeName = new Dictionary<GameObject, string>();

        bulletPoolConfigs.pool = new Queue<GameObject>();

        GameObject obj = Instantiate(bulletPoolConfigs.GunTypes.BulletPrefab, transform);
        obj.SetActive(false);
        bulletPoolConfigs.pool.Enqueue(obj);
        objectToBulletTypeName[obj] = bulletPoolConfigs.bulletTypeName;


        poolConfigDictionary[bulletPoolConfigs.bulletTypeName] = bulletPoolConfigs;
    }
    
    public GameObject Spawn(string bulletTypeName, Vector3 position)
    {
        if (!poolConfigDictionary.TryGetValue(bulletTypeName, out SpawnBulletData config))
        {
            return null;
        }

        if (config.pool.Count == 0)
        {
            GameObject newObj = Instantiate(config.GunTypes.BulletPrefab, transform);
            newObj.SetActive(false);
            objectToBulletTypeName[newObj] = bulletTypeName;
            config.pool.Enqueue(newObj);
        }

        GameObject obj = config.pool.Dequeue();

        obj.transform.position = position;
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);

        return obj;
    }


    public void Despawn(GameObject bullet)
    {
        if (bullet == null)
            return;

        if (!objectToBulletTypeName.TryGetValue(bullet, out string bulletTypeName))
        {
            bullet.SetActive(false);
            return;
        }

        if (poolConfigDictionary.TryGetValue(bulletTypeName, out SpawnBulletData config))
        {

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            bullet.SetActive(false);
            config.pool.Enqueue(bullet);
        }
        else
        {
            bullet.SetActive(false);
        }
    }

    public BulletTypes_SO GetBulletData(string bulletTypeName)
    {
        if (poolConfigDictionary.TryGetValue(bulletTypeName, out SpawnBulletData config))
        {
            return config.GunTypes;
        }
        return null;
    }
}

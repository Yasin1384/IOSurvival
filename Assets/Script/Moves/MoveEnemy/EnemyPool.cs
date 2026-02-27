using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 10;

    private List<GameObject> pool;

    public void Initialize(GameObject enemyPrefab, int poolSize)
    {
        this.enemyPrefab = enemyPrefab;
        this.poolSize = poolSize;

        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(this.enemyPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
    public GameObject Spawn(Vector3 position)
    {
        foreach (var enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = position;
                enemy.SetActive(true);
                return enemy;
            }
        }

        return null;
    }

    public void Despawn(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}

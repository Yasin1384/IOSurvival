using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform ground;
    public int currentCount = 0;
    public int maxEnemies = 10;


    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        Vector3 pos = GetRandomPointOnGround();
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
    Vector3 GetRandomPointOnGround()
    {
        Renderer groundRenderer = ground.GetComponent<Renderer>();
        Bounds bounds = groundRenderer.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        float y = bounds.max.y;

        return new Vector3(x, y, z);
    }
}

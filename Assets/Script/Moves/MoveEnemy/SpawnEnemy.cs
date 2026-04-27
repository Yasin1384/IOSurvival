using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Enemy Types")]
    public List<EnemyType_SO> enemyTypes;

    private Dictionary<EnemyType_SO, EnemyPool> pools;

    [SerializeField] private int poolSize = 10;

    private float[] _spawnTimes = { 1f, 2 };
   

    private Coroutine _spawnCoroutine;

    [SerializeField] private MeshCollider _spawnArea;

    [SerializeField] private TimerGame _timerGame;
    private EnemyPool enemyPool;
    [SerializeField] private int maxEnemies = 10;

    private void Awake()
    {
        pools = new Dictionary<EnemyType_SO, EnemyPool>();

        foreach (var type in enemyTypes)
        {
            EnemyPool pool = gameObject.AddComponent<EnemyPool>();
            pool.Initialize(type.EnemyPrefab, poolSize);
            pools.Add(type, pool);
        }
    }

    private void OnEnable()
    {
        if (_timerGame != null)
        {
            _timerGame.OnMinutePassed += HandleMinutePassed;
            _timerGame.OnTwoMinutesLeft += HandleTwoMinuteLeft;
            _timerGame.OnTwoMinutesLeft += HandleOneMinuteLeft;
        }
    }

    private void OnDisable()
    {
        if (_timerGame != null)
        {
            _timerGame.OnMinutePassed -= HandleMinutePassed;
            _timerGame.OnTwoMinutesLeft -= HandleTwoMinuteLeft;
            _timerGame.OnTwoMinutesLeft -= HandleOneMinuteLeft;
        }
    }



    void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private void Spawn()
    {
        EnemyType_SO selectedType = enemyTypes[Random.Range(0, enemyTypes.Count)];

        Vector3 pos = GetRandomPosition();

        GameObject obj = pools[selectedType].Spawn(pos);

        if (obj != null)
        {
            obj.GetComponent<EnemyDamage>().Init(selectedType);
            obj.GetComponent<MovementEnemy>().SpeedSnemies(selectedType);
            
        }
    }

    private Vector3 GetRandomPosition()
    {
        Transform t = _spawnArea.transform;

        float width = 10f * t.localScale.x;
        float length = 10f * t.localScale.z;

        float x = Random.Range(-width / 2f, width / 2f);
        float z = Random.Range(-length / 2f, length / 2f);

        return t.position + new Vector3(x, 0f, z);
    }
    private IEnumerator SpawnEnemies()
    {
        Debug.Log(_spawnTimes);
        while (true)
        {
            float randomTime = _spawnTimes[Random.Range(0, _spawnTimes.Length)];
            yield return new WaitForSeconds(randomTime);
            Spawn();
        }
    }

    private void HandleMinutePassed(int minuteRemaining)
    {

    }

    private void HandleTwoMinuteLeft()
    {
        _spawnTimes = new float[] { 0.5f, 0.7f };
    }

    private void HandleOneMinuteLeft()
    {
        _spawnTimes = new float[] { 0.2f, 0.4f };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Enemy Types")]
    private Dictionary<EnemyType_SO, EnemyPool> pools;
    EnemyType enemyType;
    
    private TimerGame _timerGame;
    private Coroutine _spawnCoroutine;
    private EnemyPool enemyPool;
    private Transform playerTransform;
    
    
    [SerializeField] private int poolSize;
    [SerializeField] private float[] _spawnTimes;
    [SerializeField] private MeshCollider _spawnArea;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float minDistanceFromPlayer;
    [SerializeField] private int maxSpawnTries;







    private void Awake()
    {
        List<EnemyType_SO> enemyTypes = GameManager.Instance.EnemyTypes; 
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
        _timerGame = GameManager.Instance.timerGame;

        if (_timerGame != null)
        {
            _timerGame.OnMinutePassed += HandleMinutePassed;
            _timerGame.OnTwoMinutesLeft += HandleTwoMinuteLeft;
            _timerGame.OnTwoMinutesLeft += HandleOneMinuteLeft;
        }
    }

    private void OnDisable()
    {
        _timerGame = GameManager.Instance.timerGame;

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

        List<EnemyType_SO> enemyTypes = GameManager.Instance.EnemyTypes;

        EnemyType_SO selectedType = enemyTypes[Random.Range(0, enemyTypes.Count)];

        Vector3 pos = GetRandomPosition();

        GameObject obj = pools[selectedType].Spawn(pos);

        if (obj != null)
        {
            obj.GetComponent<EnemyDamage>().Init(selectedType);
            obj.GetComponent<MovementEnemy>().SetupEnemyType(enemyType, selectedType);            
        }
    }

    private Vector3 GetRandomPosition()
    {
        Transform t = _spawnArea.transform;

        float width = 100f * t.localScale.x;
        float length = 100f * t.localScale.z;

        for (int i = 0; i < maxSpawnTries; i++)
        {
            float x = Random.Range(-width / 2f, width / 2f);
            float z = Random.Range(-length / 2f, length / 2f);

            Vector3 pos = t.position + new Vector3(x, 0f, z);

            if (playerTransform == null)
                return pos;

            float distance = Vector3.Distance(pos, playerTransform.position);

            if (distance >= minDistanceFromPlayer)
            {
                return pos;
            }
        }

        return t.position;
    }
    private IEnumerator SpawnEnemies()
    {
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

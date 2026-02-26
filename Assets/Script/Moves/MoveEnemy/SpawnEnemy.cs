using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform[] Ground;
    public int CurrentCount = 0;
    public int MaxEnemies = 10;

    private float[] _spawnTimes = { 1f, 2 };

    private Coroutine _spawnCoroutine;

    [SerializeField] private MeshCollider _spawnArea;

    [SerializeField] private TimerGame _timerGame;

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

    void SpawnObjects()
    {
        Transform plane = _spawnArea.transform;

        float planeWidth = 10f * plane.localScale.x;
        float planeLength = 10f * plane.localScale.z;

        float randomX = Random.Range(-planeWidth / 2f, planeWidth / 2f);
        float randomZ = Random.Range(-planeLength / 2f, planeLength / 2f);

        Vector3 spawnPos = plane.position + new Vector3(randomX, 0f, randomZ);

        Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);

    }
    private IEnumerator SpawnEnemies()
    {
        Debug.Log(_spawnTimes);
        while (true)
        {
            float randomTime = _spawnTimes[Random.Range(0, _spawnTimes.Length)];
            yield return new WaitForSeconds(randomTime);
            SpawnObjects();
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

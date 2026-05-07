using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Gun : MonoBehaviour, ISavePlayer
{
    private const string SAVE_KEY = "DATAPLAYER_SAVE";

    [SerializeField] private AutoAim _autoAim;
    [SerializeField] private int poolSize = 50;

    [SerializeField] private GameObject bulletPrefab;

    private List<BulletPool> pools = new List<BulletPool>();

    private BulletPool bulletPool;

    [SerializeField] private Transform defaultPosition;

    private float _spawnTimes;
    private float _SpeedBullet;



    private void Awake()
    {
        LoadSpeedBullet();

        PlayerType_SO playerType = GameManager.Instance.PlayerType;
        _spawnTimes = playerType.SpeedSpawnBullet;
        _SpeedBullet = playerType.BulletSpeed;
        pools = new List<BulletPool>();

        BulletPool pool = gameObject.AddComponent<BulletPool>();
        pool.Initialize(bulletPrefab, poolSize);
        pools.Add(pool);

        bulletPool = pool;
        StartCoroutine(SpawnBullets());
        SaveSpeedBullet();

    }

    private void Spawn()
    {
        GameObject target = _autoAim.FindNearestEnemyInRange();


        if (target != null)
        {
            Vector3 predictedPos = _autoAim.PredictEnemyPosition(target);

            GameObject bulletInstance = bulletPool.Spawn(defaultPosition.position);

            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            
            bullet.SetPool(bulletPool);

            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            rb.useGravity = false;

            Vector3 direction = (predictedPos - defaultPosition.position).normalized;

            rb.linearVelocity = direction * _SpeedBullet;

            bulletInstance.transform.forward = direction;

        }
    }
    private IEnumerator SpawnBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTimes);
            Spawn();
        }
    }


    private void SaveSpeedBullet()
    {
        SavePlayerData data = new SavePlayerData();
        WriteToSaveData(data);

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadSpeedBullet()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SavePlayerData data = JsonUtility.FromJson<SavePlayerData>(json);

        ReadFromSaveData(data);
    }

    public void WriteToSaveData(SavePlayerData data)
    {
        data.BulletSpeed = _SpeedBullet;
        data.SpeedSpawnBullet= _spawnTimes;
    }

    public void ReadFromSaveData(SavePlayerData data)
    {
        _SpeedBullet = data.BulletSpeed;
        _spawnTimes = data.SpeedSpawnBullet;
    }
}

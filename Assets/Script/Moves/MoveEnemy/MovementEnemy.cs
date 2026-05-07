using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class MovementEnemy : MonoBehaviour
{
    private const string SAVE_KEY = "DATAENEMY_SAVE";

    public string playerTag = "";

    private Transform player;
    private EnemyPool enemyPool;
    private EnemyType_SO enemyType_SO;
    
    private Vector3 moveDir;

    public NavMeshAgent NavMeshAgentAi;



    void Start()
    {
        LoadSpeed();
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            player = playerObj.transform;

        enemyPool = FindObjectOfType<EnemyPool>();
    }

    void Update()
    {
        SpeedSnemies(enemyType_SO);
    }

    public void SpeedSnemies(EnemyType_SO enemyType_SO)
    {
        this.enemyType_SO = enemyType_SO;

        NavMeshAgentAi.speed = enemyType_SO.Speed;

        SaveSpeed();

        if (player != null)
        {
            Vector3 targetPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);

            NavMeshAgentAi.SetDestination(targetPlayer);
        }
    }
    public void Die()
    {
        enemyPool.Despawn(gameObject);
    }


    private void SaveSpeed()
    {
        SaveEnemyData data = new SaveEnemyData();
        WriteToSaveData(data);

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadSpeed()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        SaveEnemyData data = JsonUtility.FromJson<SaveEnemyData>(json);

        ReadFromSaveData(data);
    }

    public void WriteToSaveData(SaveEnemyData data)
    {
        data.Speed = NavMeshAgentAi.speed;
    }

    public void ReadFromSaveData(SaveEnemyData data)
    {
        NavMeshAgentAi.speed = data.Hp;
    }
}

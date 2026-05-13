using Unity.Android.Gradle.Manifest;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Progress;

public class MovementEnemy : MonoBehaviour
{
    public string playerTag = "";

    private Transform player;
    private EnemyPool enemyPool;
    private EnemyType_SO enemyType_SO;

    private Vector3 moveDir;

    public NavMeshAgent NavMeshAgentAI;

    void Start()
    {
        var enemyManager = EnemyManager.Instance;

        enemyManager.LoadSpeedEnemy();

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
        var enemyManager = EnemyManager.Instance;


        this.enemyType_SO = enemyType_SO;

        NavMeshAgentAI.speed = enemyType_SO.Speed;

        enemyManager.Speed = NavMeshAgentAI.speed;

        enemyManager.SaveSpeedEnemy();

        if (player != null)
        {
            Vector3 targetPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);

            NavMeshAgentAI.SetDestination(targetPlayer);

        }
    }

    public void Die()
    {
        Vector3 deathPosition = transform.position;
        EnemyManager.Instance.DropCoin(deathPosition);
        enemyPool.Despawn(gameObject);
    }
}

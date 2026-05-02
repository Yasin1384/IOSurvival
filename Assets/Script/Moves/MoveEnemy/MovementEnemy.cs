using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class MovementEnemy : MonoBehaviour
{
    public string playerTag = "";

    private Transform player;
    private EnemyPool enemyPool;
    private EnemyType_SO enemyType_SO;
    
    private Vector3 moveDir;

    public NavMeshAgent NavMeshAgentAi;

    void Start()
    {
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
}

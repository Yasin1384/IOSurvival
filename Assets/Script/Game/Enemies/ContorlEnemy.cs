using UnityEngine;
using UnityEngine.AI;

public class ContorlEnemy : MonoBehaviour
{
    public float stopDistance = 10f;

    public string playerTag = "";

    private Transform player;
    private EnemyPool enemyPool;
    private EnemyType_SO enemyType_SO;

    public NavMeshAgent NavMeshAgentAI;

    void Start()
    {
        var enemyManager = EnemyManager.Instance;

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

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            NavMeshAgentAI.isStopped = false;
            if (player != null)
            {
                Vector3 targetPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);

                NavMeshAgentAI.SetDestination(targetPlayer);

            }
        }
        else
        {
            NavMeshAgentAI.isStopped = true;
        }
    }

    public void Die()
    {
        Vector3 deathPosition = new Vector3(transform.position.x, 1, transform.position.z);
        EnemyManager.Instance.DropCoin(deathPosition);
        enemyPool.Despawn(gameObject);
    }
}

using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class MovementEnemy : MonoBehaviour
{
    public string playerTag = "";

    private Transform player;
    private EnemyPool enemyPool;
    private EnemyType_SO enemyType_SO;

    public NavMeshAgent NavMeshAgentAI;
    public float stopDistance = 10f;

    public EnemyType EnemyTypes;


    void OnEnable()
    {
        if (NavMeshAgentAI == null) return;

        NavMeshAgentAI.enabled = false;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 3f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }

        NavMeshAgentAI.enabled = true;
        NavMeshAgentAI.Warp(transform.position);
    }
    void Start()
    {
        var enemyManager = EnemyManager.Instance;

        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            player = playerObj.transform;

        enemyPool = FindObjectOfType<EnemyPool>();
        
        SetupEnemyType(EnemyTypes, enemyType_SO);

    }

    private void Update()
    {
        if (player == null)
        {
            Debug.Log("player is null");
            return;
        }

        if (enemyType_SO == null)
        {
            Debug.Log("enemyType_SO is null");
            return;
        }

        if (!NavMeshAgentAI.isOnNavMesh)
        {
            Debug.Log("not on navmesh");
            return;
        }
        if (player == null || enemyType_SO == null || !NavMeshAgentAI.isOnNavMesh)
            return;
        switch (EnemyTypes)
        {
            case EnemyType.EnemyWhite:
            case EnemyType.EnemyEyeBlack:
                {
                    SpeedEnemies(enemyType_SO);

                    break;
                }
            case EnemyType.EnemyGuner:
                {
                    EnemyGunner(enemyType_SO);
                    break;
                }
        }
    }

    public void SetupEnemyType(EnemyType enemyType, EnemyType_SO enemyType_SO)
    {
        if (enemyType_SO == null)
        {
            Debug.LogError("EnemyType_SO is null!");
            return;
        }

        this.enemyType_SO = enemyType_SO;

        NavMeshAgentAI.speed = enemyType_SO.Speed;
    }

    public void SpeedEnemies(EnemyType_SO enemyType_SO)
    {
        if (player != null)
        {
            Vector3 targetPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);

            NavMeshAgentAI.SetDestination(targetPlayer);
        }

    }
    public void EnemyGunner(EnemyType_SO enemyType_SO)
    {
        if (!NavMeshAgentAI.isOnNavMesh)
        {
            Debug.LogWarning("Agent is not on NavMesh!");
            return;
        }
        if (player == null) return;
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
        Vector3 deathPosition = transform.position;
        EnemyManager.Instance.DropCoin(deathPosition);
        enemyPool.Despawn(gameObject);
    }
}



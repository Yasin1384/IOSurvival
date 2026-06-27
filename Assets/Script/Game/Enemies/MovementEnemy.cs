using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Services.Analytics;
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


    [SerializeField] private Animator animator;

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
        GetComponent<BoxCollider>().enabled = true;

        animator.SetBool("Looser", false);

        var enemyManager = EnemyManager.Instance;

        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            player = playerObj.transform;

        enemyPool = FindObjectOfType<EnemyPool>();
        
        SetupEnemyType(EnemyTypes, enemyType_SO);

    }

    private void Update()
    {
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
        NavMeshAgentAI.isStopped = true;
        GetComponent<BoxCollider>().enabled = false;
        Vector3 deathPosition = new Vector3(transform.position.x , 1, transform.position.z);
        EnemyManager.Instance.DropCoin(deathPosition);
        animator.SetBool("Looser", true);
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1.5f);
        enemyPool.Despawn(gameObject);
    }
}



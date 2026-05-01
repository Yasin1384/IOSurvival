using Unity.Burst.CompilerServices;
using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    public string playerTag = "";

    private Transform player;
    private EnemyPool enemyPool;
    private EnemyType_SO enemyType_SO;

    private PathFinding obstacleManager;
    public float avoidStrength;

    [Header("Avoidance")]
    public float detectDistanceMultiplier;
    public float sphereRadius;
    
    private Vector3 moveDir;

    public LayerMask obstacleMask;

    private Vector3 lockedDir;
    private float lockTime = 0f;
    public float lockDuration;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        obstacleManager = FindObjectOfType<PathFinding>();
        if (playerObj != null)
            player = playerObj.transform;

        enemyPool = FindObjectOfType<EnemyPool>();
    }

    void Update()
    {
        if (player == null || enemyType_SO == null) return;

        MoveToPlayer();
    }
    private void MoveToPlayer()
    {
        float speed = enemyType_SO.Speed;

        Vector3 origin = transform.position + Vector3.up * 0.5f;

        Vector3 toPlayer = (player.position - transform.position);
        toPlayer.y = 0;
        toPlayer.Normalize();

        Vector3 forward = toPlayer;

        Vector3 desiredDir = forward;

        float detectDistance = speed * 1f;

        if (lockTime > 0)
        {
            lockTime -= Time.deltaTime;
            desiredDir = lockedDir;
        }
        else
        {
            bool blocked = Physics.SphereCast(origin, sphereRadius, forward, out RaycastHit hit, detectDistance, obstacleMask);

            if (blocked)
            {
                Vector3 left = Quaternion.Euler(0, -45, 0) * forward;
                Vector3 right = Quaternion.Euler(0, 45, 0) * forward;

                bool leftBlocked = Physics.SphereCast(origin, sphereRadius, left, out RaycastHit hit1, detectDistance, obstacleMask);
                bool rightBlocked = Physics.SphereCast(origin, sphereRadius, right, out RaycastHit hit2, detectDistance, obstacleMask);

                if (!leftBlocked)
                {
                    desiredDir = left;
                }
                else if (!rightBlocked)
                {
                    desiredDir = right;
                }
                else
                {
                    desiredDir = -forward;
                }

                lockedDir = desiredDir;
                lockTime = lockDuration;
            }
        }

        moveDir = Vector3.Lerp(moveDir, desiredDir, 1f * Time.deltaTime);

        transform.position += moveDir * speed * Time.deltaTime;

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

    }

    public void SetEnemyType(EnemyType_SO type)
    {
        enemyType_SO = type;
    }

    public void Die()
    {
        enemyPool.Despawn(gameObject);
    }
}

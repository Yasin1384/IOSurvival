using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    public string playerTag = "";

    private Transform player;
    private EnemyPool enemyPool;

    private EnemyType_SO enemyType_SO;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("Player with tag '" + playerTag + "' not found!");

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
            Vector3 direction = player.position - transform.position;
            direction.y = 0;

            transform.position += direction.normalized * enemyType_SO.Speed * Time.deltaTime;

            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }

    public void Die()
    {
        enemyPool.Despawn(gameObject);
    }
}

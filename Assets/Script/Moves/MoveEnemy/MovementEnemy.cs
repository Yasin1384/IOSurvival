using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    //public string playerTag = "Player";
    //public float speed = 5f;
    //private Transform player;

    //void Start()
    //{
    //    GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
    //    if (playerObj != null)
    //        player = playerObj.transform;
    //    else
    //        Debug.LogWarning("Player with tag '" + playerTag + "' not found!");
    //}

    //void Update()
    //{
    //    if (player != null)
    //    {
    //        Vector3 direction = player.position - transform.position;
    //        direction.y = 0;

    //        transform.position += direction.normalized * speed * Time.deltaTime;

    //        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    //    }
    //}
    public string playerTag = "Player";
    public float speed = 5f;

    private Transform player;
    private EnemyPool enemyPool;

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
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0;

            transform.position += direction.normalized * speed * Time.deltaTime;

            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }

    public void Die()
    {
        enemyPool.Despawn(gameObject);
    }
}

using UnityEngine;

public class AutoAimEnemy : MonoBehaviour
{
    public float detectionRadius = 100f;
    public float rotationSpeed = 5f;
    public float bulletSpeed = 20f;

    private Rigidbody rb;

    private Vector3 lastEnemyPosition;
    private GameObject currentTarget;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogWarning("AutoAim requires a Rigidbody.");
        }
    }

    void FixedUpdate()
    {
        currentTarget = FindNearestPlayerInRange();

        if (currentTarget != null)
        {
            RotateToPlayer(currentTarget);
        }
    }

    public GameObject FindNearestPlayerInRange()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float minDistance = detectionRadius;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = player;
            }
        }

        return nearest;
    }

    public void RotateToPlayer(GameObject player)
    {
        Vector3 predictedPosition = PredictPlayerPosition(player);

        Vector3 originPos = (rb != null) ? rb.position : transform.position;

        Vector3 direction = predictedPosition - originPos;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        direction.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        if (rb != null)
        {
            rb.MoveRotation(
                Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed)
            );
        }
        else
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed
            );
        }
    }

    public Vector3 PredictPlayerPosition(GameObject player)
    {
        Vector3 currentPos = player.transform.position;

        if (player != currentTarget)
        {
            lastEnemyPosition = currentPos;
            return currentPos;
        }

        Vector3 velocity = (currentPos - lastEnemyPosition) / Time.fixedDeltaTime;

        lastEnemyPosition = currentPos;

        float distance = Vector3.Distance(transform.position, currentPos);

        float timeToHit = distance / bulletSpeed;

        return currentPos + velocity * timeToHit;
    }
}

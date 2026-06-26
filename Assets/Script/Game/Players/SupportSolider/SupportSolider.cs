using UnityEngine;

public class SupportSolider : MonoBehaviour
{
    GameObject target;
    public float stopDistance = 2f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > stopDistance)
        {
            transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            5 * Time.deltaTime
            );
        }
    }
}

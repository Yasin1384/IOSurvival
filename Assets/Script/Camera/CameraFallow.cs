using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public string targetTag = "Player";
    public Vector3 offset;
    public float smoothSpeed = 10f;

    private Transform target;

    void Start()
    {
        GameObject targetObj = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObj != null)
            target = targetObj.transform;
        else
            Debug.LogWarning("Target with tag '" + targetTag + "' not found!");
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public List<GameObject> obstacles;

    private List<Bounds> obstacleBounds = new List<Bounds>();

    void Start()
    {
        UpdateBounds();
    }

    public void UpdateBounds()
    {
        obstacleBounds.Clear();

        foreach (var obj in obstacles)
        {
            if (obj == null) continue;

            Collider col = obj.GetComponent<Collider>();
            if (col != null)
            {
                obstacleBounds.Add(col.bounds);
            }
        }
    }

    public bool IsPositionBlocked(Vector3 position)
    {
        foreach (var b in obstacleBounds)
        {
            if (b.Contains(position))
                return true;
        }
        return false;
    }
}

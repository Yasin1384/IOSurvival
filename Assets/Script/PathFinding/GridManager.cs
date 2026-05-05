using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private bool useCollider = false;

    public Vector3 GetOrientedSizeWorld()
    {
        if (useCollider)
        {
            var col = GetComponent<Collider>();
            if (col == null) return Vector3.zero;

            Bounds b = col.bounds;
            Vector3 scale = transform.lossyScale;
            Quaternion rot = transform.rotation;

            Vector3 localSize = Vector3.Scale(b.size, new Vector3(1f, 1f, 1f));
            localSize = new Vector3(
                Mathf.Abs(localSize.x / Mathf.Max(0.000001f, scale.x)),
                Mathf.Abs(localSize.y / Mathf.Max(0.000001f, scale.y)),
                Mathf.Abs(localSize.z / Mathf.Max(0.000001f, scale.z))
            );

            Vector3 orientedSizeInWorld = Vector3.Scale(localSize, new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z)));
            return orientedSizeInWorld;
        }
        else
        {
            var rend = GetComponent<Renderer>();
            if (rend == null) return Vector3.zero;

            Bounds b = rend.bounds;
            Vector3 scale = transform.lossyScale;

            Vector3 localSize = new Vector3(
                Mathf.Abs(b.size.x / Mathf.Max(0.000001f, scale.x)),
                Mathf.Abs(b.size.y / Mathf.Max(0.000001f, scale.y)),
                Mathf.Abs(b.size.z / Mathf.Max(0.000001f, scale.z))
            );

            Vector3 orientedSizeInWorld = Vector3.Scale(localSize, new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z)));
            return orientedSizeInWorld;
        }
    }

    public Vector3 GetOrientedSizeLocal()
    {
        if (useCollider)
        {
            var col = GetComponent<Collider>();
            if (col == null) return Vector3.zero;

            Bounds bWorld = col.bounds;
            Vector3 scale = transform.lossyScale;

            Vector3 sizeLocal = new Vector3(
                Mathf.Abs(bWorld.size.x / Mathf.Max(0.000001f, scale.x)),
                Mathf.Abs(bWorld.size.y / Mathf.Max(0.000001f, scale.y)),
                Mathf.Abs(bWorld.size.z / Mathf.Max(0.000001f, scale.z))
            );

            return sizeLocal;
        }
        else
        {
            var rend = GetComponent<Renderer>();
            if (rend == null) return Vector3.zero;

            Bounds bWorld = rend.bounds;
            Vector3 scale = transform.lossyScale;

            Vector3 sizeLocal = new Vector3(
                Mathf.Abs(bWorld.size.x / Mathf.Max(0.000001f, scale.x)),
                Mathf.Abs(bWorld.size.y / Mathf.Max(0.000001f, scale.y)),
                Mathf.Abs(bWorld.size.z / Mathf.Max(0.000001f, scale.z))
            );

            return sizeLocal;
        }
    }

    public Vector3 GetWorldCenter()
    {
        if (useCollider)
        {
            var col = GetComponent<Collider>();
            return col ? col.bounds.center : Vector3.zero;
        }
        else
        {
            var rend = GetComponent<Renderer>();
            return rend ? rend.bounds.center : Vector3.zero;
        }
    }
}

using UnityEngine;

public class Node
{
    public Vector3 worldPosition;
    public bool walkable;

    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost => gCost + hCost;

    public int x, y, z;

    public Node(Vector3 pos, bool walkable, int x, int y, int z)
    {
        this.worldPosition = pos;
        this.walkable = walkable;
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

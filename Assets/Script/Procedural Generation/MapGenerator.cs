using TreeEditor;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float tileSize = 1f;

    public GameObject treePrefab;
    public GameObject bushPrefab;

    public Transform parent;

    private TileType[,] mapData;


    public float xOffset = 0f;
    public float yOffset = 0f;
    public float zOffset = 0f;
    
    [Range(0f, 1f)]
    public float percentage;
    public float centerExclusionRadius = 1f;


    public NavMeshSurface meshSurface;


    void Start()
    {
        InitializeMap();

        GenerateForestTiles();

        BuildMap();
    }

    void InitializeMap()
    {
        mapData = new TileType[mapWidth, mapHeight];
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                mapData[x, y] = TileType.Ground;
            }
        }
    }

    void GenerateForestTiles()
    {
        Vector2 mapCenter = new Vector2(mapWidth / 2f, mapHeight / 2f);

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector2 currentPosition = new Vector2(x, y);
                if (Vector2.Distance(currentPosition, mapCenter) > centerExclusionRadius)
                {
                    if (mapData[x, y] == TileType.Ground && Random.value < percentage)
                    {
                        mapData[x, y] = TileType.Tree;
                    }
                }
            }
        }



        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector2 currentPosition = new Vector2(x, y);
                if (Vector2.Distance(currentPosition, mapCenter) > centerExclusionRadius)
                {
                    if (mapData[x, y] == TileType.Ground && Random.value < percentage)
                    {
                        mapData[x, y] = TileType.Bush;
                    }
                }
            }
        }

    }

    void BuildMap()
    {
        
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, y * tileSize);
                TileType currentTileType = mapData[x, y];
                GameObject tileObject = null;

                switch (currentTileType)
                {
                    case TileType.Ground:
                        break;
                    case TileType.Tree:
                        if (treePrefab != null)
                        {
                            Vector3 treePosition = position + new Vector3(xOffset, yOffset, zOffset);
                            Quaternion treeRotation = Quaternion.Euler(0, Random.Range(0f, 0f), 0);
                            tileObject = Instantiate(treePrefab, treePosition, treeRotation, parent);
                        }
                        break;
                    case TileType.Bush:
                        {
                            Vector3 bushPosition = position + new Vector3(xOffset, yOffset, zOffset);
                            Quaternion bushRotation = Quaternion.Euler(0, Random.Range(0f, 0), 0);
                            tileObject = Instantiate(bushPrefab, bushPosition, bushRotation, parent);
                            meshSurface.BuildNavMesh();
                        }
                        break;
                    case TileType.Rock:
                        break;
                }

                if (tileObject != null)
                {
                    tileObject.name = $"Tile_{x}_{y}_{currentTileType}";
                }
            }
        }
    }

    public TileType GetTileType(int x, int y)
    {
        if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
        {
            return mapData[x, y];
        }
        return TileType.Empty;
    }

    public void SetTileType(int x, int y, TileType type)
    {
        if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
        {
            mapData[x, y] = type;
        }
    }
}

public enum TileType
{
    Empty,
    Ground,
    Tree,
    Bush,
    Rock,
}

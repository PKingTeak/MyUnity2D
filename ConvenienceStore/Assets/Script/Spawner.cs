using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitSpawner : MonoBehaviour
{
    [Header("소환 가능한 유닛")]
    [SerializeField] private List<GameObject> unitPrefabs;

    [Header("소환 타일맵")]
    [SerializeField] private Tilemap tilemap;                 // 사용할 타일맵
    [SerializeField] private TileBase spawnableTile;          // 이 타일 위에서만 소환됨

    private List<Vector2> spawnPoints = new List<Vector2>();  // 자동으로 수집됨
    private HashSet<Vector2> occupiedPoints = new HashSet<Vector2>();

    [SerializeField] private int spawnCount = 5;

    private void Start()
    {
        FindSpawnableTiles();
        SpawnRandomUnits();
    }

    /// <summary>
    /// 타일맵에서 지정된 spawnableTile을 찾고, spawnPoints에 등록
    /// </summary>
    private void FindSpawnableTiles()
    {
        spawnPoints.Clear();

        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(cellPos);

                if (tile == spawnableTile)
                {
                    Vector3 worldPos = tilemap.CellToWorld(cellPos) + tilemap.cellSize / 2f;
                    spawnPoints.Add(new Vector2(worldPos.x, worldPos.y));
                }
            }
        }
    }

    /// <summary>
    /// 사용 가능한 위치에 랜덤 유닛 소환
    /// </summary>
    public void SpawnRandomUnits()
    {
        int count = Mathf.Min(spawnCount, spawnPoints.Count - occupiedPoints.Count);

        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPos = GetRandomAvailablePosition();
            if (spawnPos == Vector2.zero) break;

            GameObject prefab = unitPrefabs[Random.Range(0, unitPrefabs.Count)];

            GameObject newUnit = Instantiate(prefab, spawnPos, Quaternion.identity);
            occupiedPoints.Add(spawnPos);
        }
    }

    private Vector2 GetRandomAvailablePosition()
    {
        List<Vector2> available = new List<Vector2>();

        foreach (var pos in spawnPoints)
        {
            if (!occupiedPoints.Contains(pos))
                available.Add(pos);
        }

        if (available.Count == 0)
            return Vector2.zero;

        return available[Random.Range(0, available.Count)];
    }

    public void UnregisterUnit(Vector2 position)
    {
        occupiedPoints.Remove(position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (spawnPoints != null)
        {
            foreach (var point in spawnPoints)
            {
                Gizmos.DrawWireSphere(new Vector3(point.x, point.y, 0f), 0.3f);
            }
        }
    }
}

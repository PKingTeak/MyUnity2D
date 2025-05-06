using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitSpawner : MonoBehaviour
{
    [Header("��ȯ ������ ����")]
    [SerializeField] private List<GameObject> unitPrefabs;

    [Header("��ȯ Ÿ�ϸ�")]
    [SerializeField] private Tilemap tilemap;                 // ����� Ÿ�ϸ�
    [SerializeField] private TileBase spawnableTile;          // �� Ÿ�� �������� ��ȯ��

    private List<Vector2> spawnPoints = new List<Vector2>();  // �ڵ����� ������
    private HashSet<Vector2> occupiedPoints = new HashSet<Vector2>();

    [SerializeField] private int spawnCount = 5;


    public Tilemap Tilemap => tilemap;

    private void Start()
    {
        FindSpawnableTiles();
        SpawnRandomUnits();
        StartCoroutine(AutoRespawn());
    }

    /// <summary>
    /// Ÿ�ϸʿ��� ������ spawnableTile�� ã��, spawnPoints�� ���
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
    /// ��� ������ ��ġ�� ���� ���� ��ȯ
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

    private IEnumerator AutoRespawn()
    {
        WaitForSeconds delay = new WaitForSeconds(2f);

        while (true)
        {
            SpawnRandomUnits();
            yield return delay;

        }
    }


    public void UnregisterUnitByWorldPosition(Vector3 worldPosition)
    {
        Vector3Int cellPos = tilemap.WorldToCell(worldPosition);
        Vector3 normalizedWorld = tilemap.CellToWorld(cellPos) + tilemap.cellSize / 2f;
        Vector2 pos = new Vector2(normalizedWorld.x, normalizedWorld.y);

        UnregisterUnit(pos);
    }


}

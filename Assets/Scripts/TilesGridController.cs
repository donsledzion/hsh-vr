using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesGridController : MonoBehaviour
{
    [Tooltip("Grid dimensions in meters")]
    [SerializeField] float gridWidth = 15f, gridHeight = 15f; // default grid dimensions in meters
    [Space]
    [SerializeField] public float tileSize = 0.25f;
    [SerializeField] float wallPanelSize = 0.5f;
    [SerializeField] float spacing = 0.001f; // default spacing between tiles
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject tilesGridContainer;
    [SerializeField] GameObject snapPointsContainer;
    [SerializeField] GameObject snapPointPrefab;
    [SerializeField] float snapPointScaleFactor = 30f;
    [Space]
    [SerializeField] int rows;
    [SerializeField] int cols;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            GenerateGrid();
        }
    }

    public Vector2 GetGridSize()
    {
        return new Vector2(gridWidth, gridHeight);
    }

    public void GenerateGrid(float width, float height)
    {
        gridWidth = width;
        gridHeight = height;
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        rows = (int)(gridWidth / tileSize);
        cols = (int)(gridHeight / tileSize);

        Vector3 tileSizeOffset = new Vector3((tileSize) / 2, 0, (tileSize) / 2);

        Vector3 sizeOffset = new Vector3( -cols * (tileSize) / 2, 0f, -rows * (tileSize) / 2);

        Vector3 snapPointScale = new Vector3(spacing, spacing, spacing) * snapPointScaleFactor;

        Vector3 tileScale = new Vector3(tileSize, tilePrefab.transform.localScale.y, tileSize);

        SnapPointType pointType = SnapPointType.Default;

        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                pointType = SnapPointType.Default;
                Vector3 spawnPos = new Vector3( i * (tileSize), tilePrefab.transform.localScale.y,j * (tileSize)) + sizeOffset;
                GameObject newTile = Instantiate(tilePrefab,spawnPos,tilePrefab.transform.rotation);
                newTile.transform.localScale = tileScale;
                newTile.transform.SetParent(tilesGridContainer.transform);

                if ((j % 2) != 0 && (i % 2) != 0)
                    pointType = SnapPointType.WallGrid;

                InstantiateSnapPoint(spawnPos, tileSizeOffset, snapPointScale, pointType);                
            }
        }

        for (int i = 0; i < rows; i++)
        {
            Vector3 spawnPos = new Vector3(
                       i * (tileSize),
                       tilePrefab.transform.localScale.y,
                       -1 * (tileSize)) + sizeOffset;
            if ((i % 2) != 0)
                pointType = SnapPointType.WallGrid;
            else
                pointType = SnapPointType.Default;
            InstantiateSnapPoint(spawnPos, tileSizeOffset, snapPointScale, pointType);
        }

        for (int i = 0; i < cols; i++)
        {
            Vector3 spawnPos = new Vector3(
                       -1 * (tileSize),
                       tilePrefab.transform.localScale.y,
                       i * (tileSize)) + sizeOffset;
            if ((i % 2) != 0)
                pointType = SnapPointType.WallGrid;
            else 
                pointType = SnapPointType.Default;
            InstantiateSnapPoint(spawnPos, tileSizeOffset, snapPointScale, pointType);
        }

        Vector3 firstSpawnPos = new Vector3(-1 * (tileSize),tilePrefab.transform.localScale.y,-1 * (tileSize)) + sizeOffset;
        InstantiateSnapPoint(firstSpawnPos, tileSizeOffset, snapPointScale, SnapPointType.WallGrid);

    }

    void InstantiateSnapPoint(Vector3 _spawnPos, Vector3 _tileSizeOffset, Vector3 _snapPointScale, SnapPointType _type)
    {
        GameObject snapPoint = Instantiate(snapPointPrefab, _spawnPos + _tileSizeOffset, snapPointPrefab.transform.rotation);
        snapPoint.transform.localScale = _snapPointScale;        
        snapPoint.transform.SetParent(snapPointsContainer.transform);
        snapPoint.GetComponent<SnapPoint>().SetType(_type);
    }
}

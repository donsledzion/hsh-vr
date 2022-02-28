using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesGridController : MonoBehaviour
{
    [Tooltip("Grid dimensions in meters")]
    [SerializeField] float gridWidth = 15f, gridHeight = 15f; // default grid dimensions in meters
    [SerializeField] float spacing = 0.001f; // default spacing between tiles
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject tilesGridContainer;
    [SerializeField] GameObject snapPointsContainer;
    [SerializeField] GameObject snapPointPrefab;
    [SerializeField] float snapPointScaleFactor = 10f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            GenerateGrid();
        }
    }

    public void GenerateGrid()
    {
        int rows = (int)(gridWidth / tilePrefab.transform.localScale.z);
        int cols = (int)(gridHeight / tilePrefab.transform.localScale.x);

        Vector3 tileSizeOffset = new Vector3(
            (tilePrefab.transform.localScale.x+spacing)/2,
            0,
            (tilePrefab.transform.localScale.z + spacing) / 2);

        Vector3 sizeOffset = new Vector3(
                    -cols * (tilePrefab.transform.localScale.x + spacing) / 2,
                    0f,
                    -cols * (tilePrefab.transform.localScale.z + spacing) / 2);

        Vector3 snapPointScale = new Vector3(spacing, spacing, spacing) * snapPointScaleFactor;

        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                Vector3 spawnPos = new Vector3(
                    i * (tilePrefab.transform.localScale.x+spacing),
                    tilePrefab.transform.localScale.y,
                    j * (tilePrefab.transform.localScale.z+spacing)) + sizeOffset;
                GameObject newTile = Instantiate(tilePrefab,spawnPos,tilePrefab.transform.rotation);
                newTile.transform.SetParent(tilesGridContainer.transform);

                GameObject snapPoint = Instantiate(snapPointPrefab, spawnPos + tileSizeOffset, snapPointPrefab.transform.rotation);
                snapPoint.transform.localScale = snapPointScale;
                snapPoint.transform.SetParent(snapPointsContainer.transform);
            }
        }

        for (int i = 0; i < rows; i++)
        {
            Vector3 spawnPos = new Vector3(
                       i * (tilePrefab.transform.localScale.x + spacing),
                       tilePrefab.transform.localScale.y,
                       -1 * (tilePrefab.transform.localScale.z + spacing)) + sizeOffset;
            GameObject snapPoint = Instantiate(snapPointPrefab, spawnPos + tileSizeOffset, snapPointPrefab.transform.rotation);
            snapPoint.transform.localScale = snapPointScale;
            snapPoint.transform.SetParent(snapPointsContainer.transform);
        }

        for (int i = 0; i < cols; i++)
        {
            Vector3 spawnPos = new Vector3(
                       -1 * (tilePrefab.transform.localScale.x + spacing),
                       tilePrefab.transform.localScale.y,
                       i * (tilePrefab.transform.localScale.z + spacing)) + sizeOffset;
            GameObject snapPoint = Instantiate(snapPointPrefab, spawnPos + tileSizeOffset, snapPointPrefab.transform.rotation);
            snapPoint.transform.localScale = snapPointScale;
            snapPoint.transform.SetParent(snapPointsContainer.transform);
        }

        Vector3 firstSpawnPos = new Vector3(
                       -1 * (tilePrefab.transform.localScale.x + spacing),
                       tilePrefab.transform.localScale.y,
                       -1 * (tilePrefab.transform.localScale.z + spacing)) + sizeOffset;
        GameObject firstSnapPoint = Instantiate(snapPointPrefab, firstSpawnPos + tileSizeOffset, snapPointPrefab.transform.rotation);
        firstSnapPoint.transform.localScale = snapPointScale;
        firstSnapPoint.transform.SetParent(snapPointsContainer.transform);

    }
}

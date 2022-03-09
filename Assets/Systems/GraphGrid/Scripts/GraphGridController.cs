using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGridController : MonoBehaviour
{
    [SerializeField] public int sizeX=10, sizeY=10;
    [SerializeField] float tileSize = 0.4f;
    [SerializeField] float elevation = 0.0f;

    [SerializeField] GameObject gridPoint;
    [SerializeField] Transform wallGridContainer;
    [SerializeField] public List<GraphGridPoint> gridPoints = new List<GraphGridPoint>();

    //======================================================================================
    // experimental purpose
    public Color defaultPointColor;

    //======================================================================================

    private void Start()
    {
        GenerateGrid(sizeX, sizeY);
        ConnectWallsPoints();
        defaultPointColor = gridPoint.GetComponent<Renderer>().sharedMaterial.color;
    }
    public void GenerateGrid(int _sizeX, int _sizeY)
    {
        for(int i = 0; i < _sizeX; i++)
        {
            for(int j = 0; j < _sizeY; j++)
            {
                Vector3 spawnPoint = new Vector3(j * 2 * tileSize, elevation, i * 2 * tileSize);
                GameObject newGridPoint = Instantiate(gridPoint, spawnPoint, Quaternion.identity);
                newGridPoint.transform.SetParent(wallGridContainer);
                newGridPoint.GetComponent<GraphGridPoint>().SetPosition(new Vector2(i, j));
                gridPoints.Add(newGridPoint.GetComponent<GraphGridPoint>());
            }
        }
    }
    public void ConnectWallsPoints()
    {
        foreach (GraphGridPoint point in gridPoints)
        {
            point.ConnectNeighbours(gridPoints);
        }
    }
}

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
        DrawGrid();
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

    public void DrawGrid()
    {
        foreach(GraphGridPoint point in gridPoints)
        {
            float elevation = 0.1f;

            if (point.transform.position.x == 0f && point.transform.position.z == 0f)
            {
                point.DrawRect(
                    new Vector3(point.transform.position.x, elevation, point.transform.position.z),
                    new Vector3((sizeX - 1) * tileSize * 2, elevation, (sizeX - 1) * tileSize * 2));
            }
            else if (point.transform.position.x == 0f)
            {
                Vector3 from = new Vector3(point.transform.position.x, elevation, point.transform.position.z);
                Vector3 to = new Vector3((sizeX-1)*tileSize*2, elevation, point.transform.position.z);
                point.DrawLine(from, to);
            }
            else if(point.transform.position.z == 0f)
            {
                Vector3 from = new Vector3(point.transform.position.x, elevation, point.transform.position.z);
                Vector3 to = new Vector3(point.transform.position.x, elevation, (sizeY-1)*tileSize*2);
                point.DrawLine(from, to);
            }

        }
    }
}

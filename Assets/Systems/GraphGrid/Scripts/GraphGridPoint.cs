using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphGridPoint : MonoBehaviour
{
    public GridConnectors gridNeighbours = new GridConnectors();        
    public GridConnectors wallsConnections = new GridConnectors();        
    public Vector2 position;
    [SerializeField] LineRenderer lineRenderer;
    // ==============================================
    // BFS pathfinder purpose fields
    public bool visited;
    public GraphGridPoint cameFrom;
    public int number;
    // ==============================================

    
    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }

    public void ConnectNeighbours(List<GraphGridPoint> points)
    {
        foreach(GraphGridPoint point in points)
        {
            if (point.position.x == position.x - 1) // all "west" neighbours
            {
                if (point.position.y == position.y - 1)
                {
                    point.gridNeighbours.SE = this;
                    gridNeighbours.NW = point;
                }
                else if (point.position.y == position.y)
                {
                    point.gridNeighbours.E = this;
                    gridNeighbours.W = point;
                }
                else if (point.position.y == position.y + 1)
                {
                    point.gridNeighbours.NE = this;
                    gridNeighbours.SW = point;
                }
            }
            else if (point.position.x == position.x) // norht and south neighbours
            {
                if(point.position.y == position.y - 1)
                {
                    point.gridNeighbours.S = this;
                    gridNeighbours.N = point;
                }
                else if(point.position.y == position.y + 1)
                {
                    point.gridNeighbours.N = this;
                    gridNeighbours.S = point;
                }
            }
            else if (point.position.x == position.x) // all "east" neighbours
            {
                if (point.position.y == position.y - 1)
                {
                    point.gridNeighbours.SW = this;
                    gridNeighbours.NE = point;
                }
                else if (point.position.y == position.y)
                {
                    point.gridNeighbours.W = this;
                    gridNeighbours.E = point;
                }
                else if (point.position.y == position.y + 1)
                {
                    point.gridNeighbours.NW = this;
                    gridNeighbours.SE = point;
                }
            }
        }                
    }

    public void MarkAsVisited()
    {
        visited = true;
    }

    public void Unvisit()
    {
        visited = false;
    }

    public List<GraphGridPoint> GetNeighbours()
    {
        List<GraphGridPoint> neighbours = new List<GraphGridPoint>();

        if (gridNeighbours.N != null) neighbours.Add(gridNeighbours.N);
        if (gridNeighbours.S != null) neighbours.Add(gridNeighbours.S);
        if (gridNeighbours.E != null) neighbours.Add(gridNeighbours.E);
        if (gridNeighbours.W != null) neighbours.Add(gridNeighbours.W);

        if (gridNeighbours.NE != null) neighbours.Add(gridNeighbours.NE);
        if (gridNeighbours.NW != null) neighbours.Add(gridNeighbours.NW);
        if (gridNeighbours.SE != null) neighbours.Add(gridNeighbours.SE);
        if (gridNeighbours.SW != null) neighbours.Add(gridNeighbours.SW);

        return neighbours;
    }

    [System.Serializable]
    public struct GridConnectors
    {
        public GraphGridPoint N;
        public GraphGridPoint S;
        public GraphGridPoint E;
        public GraphGridPoint W;
        public GraphGridPoint NE;
        public GraphGridPoint NW;
        public GraphGridPoint SE;
        public GraphGridPoint SW;
    }

    public void DrawLine(Vector3 from, Vector3 to)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
    }

    public void DrawRect(Vector3 edge1, Vector3 edge3)
    {
        Vector3 edge2 = new Vector3(edge1.x,edge1.y,edge3.z);
        Vector3 edge4 = new Vector3(edge3.x,edge1.y,edge1.z);
        lineRenderer.positionCount = 5;
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.SetPosition(0, edge1);
        lineRenderer.SetPosition(1, edge2);
        lineRenderer.SetPosition(2, edge3);
        lineRenderer.SetPosition(3, edge4);
        lineRenderer.SetPosition(4, edge1);
    }

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position+Vector3.up*.3f,"(" + position.x + "," + position.y + ")");
    }
}

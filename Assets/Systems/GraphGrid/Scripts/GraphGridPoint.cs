using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

    public bool MarkNeighbour(GraphGridPoint other)
    {
        if(gridNeighbours.N == other && wallsConnections.N == null)
        {
            wallsConnections.N = other;
            other.wallsConnections.S = this;
            return true;
        }
        if (gridNeighbours.S == other && wallsConnections.S == null)
        {
            wallsConnections.S = other;
            other.wallsConnections.N = this;
            return true;
        }
        if (gridNeighbours.E == other && wallsConnections.E == null)
        {
            wallsConnections.E = other;
            other.wallsConnections.W = this;
            return true;
        }
        if (gridNeighbours.W == other && wallsConnections.W == null)
        {
            wallsConnections.W = other;
            other.wallsConnections.E = this;
            return true;
        }
        if(gridNeighbours.NE == other && wallsConnections.NE == null)
        {
            wallsConnections.NE = other;
            other.wallsConnections.SW = this;
            return true;
        }
        if (gridNeighbours.SE == other && wallsConnections.SE == null)
        {
            wallsConnections.SE = other;
            other.wallsConnections.NW = this;
            return true;
        }
        if (gridNeighbours.NW == other && wallsConnections.NW == null)
        {
            wallsConnections.NW = other;
            other.wallsConnections.SE = this;
            return true;
        }
        if (gridNeighbours.SW == other && wallsConnections.SW == null)
        {
            wallsConnections.SW = other;
            other.wallsConnections.NE = this;
            return true;
        }
        return false;
    }

    public bool HasWallTowards(GraphGridPoint other)
    {
        if(wallsConnections.N == other)            
            return true;
        if(wallsConnections.S == other)            
            return true;
        if(wallsConnections.E == other)            
            return true;
        if(wallsConnections.W == other)            
            return true;
        if(wallsConnections.NE == other)            
            return true;
        if(wallsConnections.NW == other)            
            return true;
        if(wallsConnections.SE == other)            
            return true;
        if(wallsConnections.SW == other)            
            return true;

        return false;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.Label(transform.position+Vector3.up*.3f,"(" + position.x + "," + position.y + ")");
#endif
    }
}

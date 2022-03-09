using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphGridPoint : MonoBehaviour
{
    public WallNeighbours wallNeighbours = new WallNeighbours();        
    public Vector2 position;

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
                    point.wallNeighbours.SE = this;
                    wallNeighbours.NW = point;
                }
                else if (point.position.y == position.y)
                {
                    point.wallNeighbours.E = this;
                    wallNeighbours.W = point;
                }
                else if (point.position.y == position.y + 1)
                {
                    point.wallNeighbours.NE = this;
                    wallNeighbours.SW = point;
                }
            }
            else if (point.position.x == position.x) // norht and south neighbours
            {
                if(point.position.y == position.y - 1)
                {
                    point.wallNeighbours.S = this;
                    wallNeighbours.N = point;
                }
                else if(point.position.y == position.y + 1)
                {
                    point.wallNeighbours.N = this;
                    wallNeighbours.S = point;
                }
            }
            else if (point.position.x == position.x) // all "east" neighbours
            {
                if (point.position.y == position.y - 1)
                {
                    point.wallNeighbours.SW = this;
                    wallNeighbours.NE = point;
                }
                else if (point.position.y == position.y)
                {
                    point.wallNeighbours.W = this;
                    wallNeighbours.E = point;
                }
                else if (point.position.y == position.y + 1)
                {
                    point.wallNeighbours.NW = this;
                    wallNeighbours.SE = point;
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

        if (wallNeighbours.N != null) neighbours.Add(wallNeighbours.N);
        if (wallNeighbours.S != null) neighbours.Add(wallNeighbours.S);
        if (wallNeighbours.E != null) neighbours.Add(wallNeighbours.E);
        if (wallNeighbours.W != null) neighbours.Add(wallNeighbours.W);

        if (wallNeighbours.NE != null) neighbours.Add(wallNeighbours.NE);
        if (wallNeighbours.NW != null) neighbours.Add(wallNeighbours.NW);
        if (wallNeighbours.SE != null) neighbours.Add(wallNeighbours.SE);
        if (wallNeighbours.SW != null) neighbours.Add(wallNeighbours.SW);

        return neighbours;
    }

    [System.Serializable]
    public struct WallNeighbours
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

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position+Vector3.up*.3f,"(" + position.x + "," + position.y + ")");
    }
}

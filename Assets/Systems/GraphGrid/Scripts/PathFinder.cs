using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    GraphGridController gridController;

    GraphWallBuilder wallBuilder;

    List<GraphGridPoint> gridPoints = new List<GraphGridPoint>();

    public GraphGridPoint[] endings = new GraphGridPoint[2];

    public Queue pathQueue = new Queue();


    private void Start()
    {
        wallBuilder = GameObject.Find("GraphWallBuilder").GetComponent<GraphWallBuilder>();

        gridController = gameObject.GetComponent<GraphGridController>();
        gridPoints = gridController.gridPoints;

        endings = wallBuilder.selectionPair.ToArray();
    }
    public void ClearResult()
    {
        wallBuilder.ClearSelectionPair();
        RestoreDefaultGridColor();
    }

    public void RestoreDefaultGridColor()
    {
        foreach (GraphGridPoint point in gridPoints)
            point.GetComponent<Renderer>().material.color = gridController.defaultPointColor;
    }

    public void PathFind()
    {
        PathBFS(wallBuilder.selectionPair.ToArray()[0], wallBuilder.selectionPair.ToArray()[1]);
    }

    public void PathBFS(GraphGridPoint startPoint, GraphGridPoint endPoint)
    {
        Queue<GraphGridPoint> Q = new Queue<GraphGridPoint>();        
        GraphGridPoint testedPointV;                       
        bool found = false;
        int[] path = new int[gridController.sizeX * gridController.sizeY];        
        int counter = 0;
        foreach (GraphGridPoint graphPoint in gridPoints)
        {
            graphPoint.Unvisit();
            graphPoint.GetComponent<Renderer>().material.color = Color.gray;
            graphPoint.number = counter;
            counter++;
        }

        startPoint.cameFrom = null;
        path[startPoint.number] = -1;
        Q.Enqueue(startPoint);

        startPoint.MarkAsVisited();

        while(Q.Count > 0)
        {
            testedPointV = Q.Dequeue();
            if (testedPointV == endPoint)
            {
                found = true;                
                break;
            }

            foreach(GraphGridPoint neighbour in testedPointV.GetNeighbours())
            {
                neighbour.cameFrom = testedPointV;
                if(!neighbour.visited)
                {                    
                    neighbour.MarkAsVisited();
                    path[neighbour.number] = testedPointV.number;
                    Q.Enqueue(neighbour);
                }
            }
        }

        if (!found) Debug.LogWarning("No path found");
        else
        {
            Debug.LogWarning("PATH FOUND");
            testedPointV = endPoint;
            int safetyCounter = 0;
            int v = testedPointV.number;
            while(v>-1 && safetyCounter < 1000)
            {
                foreach(GraphGridPoint point in gridPoints)
                {
                    if (point.number == v)
                    {
                        point.GetComponent<Renderer>().material.color = Color.blue;
                        break;
                    }
                }
                v = path[v];                
            }
        }

    }

}

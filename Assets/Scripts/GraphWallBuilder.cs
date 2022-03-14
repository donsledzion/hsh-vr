using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphWallBuilder : PointerSelector
{
    [SerializeField] GameObject graphWallPrefab;
    [SerializeField] GameObject wallStartPilePrefab;
    GameObject pileInstance;
    [SerializeField] PathFinder pathFinder;

    //===========================================================
    // Experimental purpose part
    Color originalColor;
    [SerializeField] public List<GraphGridPoint> selectionPair = new List<GraphGridPoint>();

    GraphGridPoint source, destination;
    List<GraphGridPoint> prototypeWallPath = new List<GraphGridPoint>();
    [SerializeField] GraphGridPoint[] prototypeWallPathArray;
    [SerializeField] Transform prototypeWallsContainer;
    [SerializeField] Transform wallsContainer;

    Transform _lastSelection;

    bool destroying = false;

    //===========================================================
    
    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.LeftControl))            
            destroying = true;
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (destroying)
                DestroyWallsOnPath();
            destroying = false;
        }

        if(!destroying)
            TryToBuild();
        else
            TryToDestroy();

        if (Input.GetMouseButtonDown(0))
        {
           if(_selection != null)
            {                
                source = _selection.gameObject.GetComponent<GraphGridPoint>();
                if(selectionPair.Count < 2)
                {
                    originalColor = _selection.gameObject.GetComponent<Renderer>().material.color;
                    _selection.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    selectionPair.Add(_selection.gameObject.GetComponent<GraphGridPoint>());                   
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            source = null;
            destination = null;

            if (destroying)
                DestroyWallsOnPath();
            else
            if (prototypeWallsContainer.childCount > 0)
            {
                for (int i = 0; i < prototypeWallPathArray.Length - 1; i++)
                {
                    prototypeWallPathArray[i].MarkNeighbour(prototypeWallPathArray[i + 1]);
                }
                MoveChildren(prototypeWallsContainer, wallsContainer);
            }
        }
        _lastSelection = _selection;
    }

    void TryToBuild()
    {
        if(!UpdatePilePosition()) return;
        pileInstance?.GetComponent<WallStartPileToggler>().SetBuilder();
        if (source != null)
        {
            destination = _selection.gameObject.GetComponent<GraphGridPoint>();
            if (_selection != _lastSelection)
            {
                pathFinder.RestoreDefaultGridColor();
                prototypeWallPath = pathFinder.PathBFS(source, destination);
                DrawWallPrototype();
            }
        }
    }

    void TryToDestroy()
    {
        if (!UpdatePilePosition()) return;
        pileInstance?.GetComponent<WallStartPileToggler>().SetDestroyer();
        if (source != null && source.HasAnyWall())
        {
            destination = _selection.gameObject.GetComponent<GraphGridPoint>();
            if (_selection != _lastSelection)
            {
                pathFinder.RestoreDefaultGridColor();
                prototypeWallPath = pathFinder.PathBFS(source, destination);
            }
        }
    }

    bool UpdatePilePosition()
    {
        if (_selection == null) return false;

        if (pileInstance == null)
            pileInstance = Instantiate(wallStartPilePrefab, _selection.position, wallStartPilePrefab.transform.rotation);
        else
            pileInstance.transform.position = _selection.position;
        return true;
    }

    void DrawWallPrototype()
    {
        ClearContainer(prototypeWallsContainer.gameObject);
        prototypeWallPathArray = new GraphGridPoint[prototypeWallPath.Count];
        prototypeWallPathArray = prototypeWallPath.ToArray();
        for(int i = 0; i < prototypeWallPathArray.Length-1; i++)
        {
            Vector3 progresVector = (prototypeWallPathArray[i + 1].transform.position - prototypeWallPathArray[i].transform.position);
            float scale = progresVector.magnitude;
            Vector3 spawnPoint = prototypeWallPathArray[i].transform.position;
            float rotation = Vector3.SignedAngle(Vector3.right, progresVector, Vector3.up);

            if (!prototypeWallPathArray[i].HasWallTowards(prototypeWallPathArray[i + 1]))
            {
                GameObject newSection = Instantiate(graphWallPrefab, spawnPoint, Quaternion.identity);

                newSection.GetComponent<WallPanel>().anchorStart = prototypeWallPathArray[i];
                newSection.GetComponent<WallPanel>().anchorEnd = prototypeWallPathArray[i+1];

                newSection.transform.localEulerAngles = newSection.transform.localEulerAngles =
                    new Vector3(newSection.transform.eulerAngles.x, rotation, newSection.transform.eulerAngles.z);
                WallPanelScaler wallPanelScaler = newSection.GetComponent<WallPanelScaler>();
                wallPanelScaler.ScaleX(scale);
                newSection.transform.SetParent(prototypeWallsContainer);
            }
        }
    }

    public void ClearContainer(GameObject container)
    {
        foreach(Transform transform in container.GetComponentsInChildren<Transform>())
        {
            if (transform != container.transform)
                Destroy(transform.gameObject);
        }
    }

    public void MoveChildren(Transform source, Transform destination)
    {
        foreach (Transform moved in source.gameObject.GetComponentsInChildren<Transform>())
        {
            if (moved.transform.parent == source.transform)
                moved.SetParent(destination);
        }
        ClearPath();
    }

    void DestroyWallsOnPath()
    {
        prototypeWallPathArray = new GraphGridPoint[prototypeWallPath.Count];
        prototypeWallPathArray = prototypeWallPath.ToArray();

        for(int i  = 0; i < prototypeWallPath.Count-1; i++)
        {
            foreach(WallPanel wall in wallsContainer.GetComponentsInChildren<WallPanel>())
            {
                if(wall.MatchAnchors(prototypeWallPathArray[i], prototypeWallPathArray[i+1]))
                {
                    Destroy(wall.gameObject);
                    prototypeWallPathArray[i].ReleaseWallBinding(prototypeWallPathArray[i + 1]);
                }
            }
        }
    }

    void ClearPath()
    {
        prototypeWallPathArray = new GraphGridPoint[0];
        prototypeWallPath.Clear();
    }

    public void ClearSelectionPair()
    {
        selectionPair.Clear();
    }
}

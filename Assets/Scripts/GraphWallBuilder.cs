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

    Transform _lastSelection;

    //===========================================================
    void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        if (_selection != null)
        {
            if (pileInstance == null)
                pileInstance = Instantiate(wallStartPilePrefab, _selection.position, wallStartPilePrefab.transform.rotation);
            else
                pileInstance.transform.position = _selection.position;
        }

        if(_selection != null)
        {
            if(source != null)
            {
                destination = _selection.gameObject.GetComponent<GraphGridPoint>();
                if (_selection != _lastSelection)
                {
                    pathFinder.RestoreDefaultGridColor();
                    pathFinder.PathBFS(source, destination);
                }

            }
        }


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
        }

        _lastSelection = _selection;
    }

    public void ClearSelectionPair()
    {
        selectionPair.Clear();
    }
}

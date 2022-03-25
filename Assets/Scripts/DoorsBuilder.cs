using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsBuilder : SectionSpawner
{
    [SerializeField] GameObject doorPrefab;

    GameObject theDoorsInstance;
    GameObject selectedPanel;
    [SerializeField] Transform tempHidden;
    [SerializeField] Transform wallsContainer;
    [SerializeField] Transform doorsContainer;

    Transform _lastSelection;

    protected override void Update()
    {
        base.Update();

        if(Input.GetMouseButtonDown(0))
        {
            if(theDoorsInstance!=null)
            {
                theDoorsInstance.transform.SetParent(doorsContainer);                
                Destroy(selectedPanel);
                theDoorsInstance = null;
            }
        }


        if (_selection != null)
        {
            if (_selection != _lastSelection)
            {
                if (theDoorsInstance != null)
                {                    
                    selectedPanel.transform.SetParent(wallsContainer);
                    Destroy(theDoorsInstance);
                }                
                selectedPanel = _selection.gameObject.GetComponentInParent<WallPanel>().gameObject;
                GraphGridPoint pointA = selectedPanel.GetComponent<WallPanel>().anchorStart;
                GraphGridPoint pointB = selectedPanel.GetComponent<WallPanel>().anchorEnd;
                theDoorsInstance = CreateSection(pointA,pointB,doorPrefab,transform.parent);
                selectedPanel.transform.SetParent(tempHidden);
            }
        }
        _lastSelection = _selection;
    }
}

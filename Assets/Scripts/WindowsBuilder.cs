using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsBuilder : SectionSpawner
{
    [SerializeField] GameObject windowPrefab;

    GameObject windowInstance;
    GameObject selectedPanel;
    [SerializeField] Transform tempHidden;
    [SerializeField] Transform wallsContainer;
    [SerializeField] Transform windowsContainer;

    Transform _lastSelection;

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            if (windowInstance != null)
            {
                windowInstance.transform.SetParent(windowsContainer);
                Destroy(selectedPanel);
                windowInstance = null;
            }
        }


        if (_selection != null)
        {
            if (_selection != _lastSelection)
            {
                if (windowInstance != null)
                {
                    selectedPanel.transform.SetParent(wallsContainer);
                    Destroy(windowInstance);
                }
                selectedPanel = _selection.gameObject.GetComponentInParent<WallPanel>().gameObject;
                GraphGridPoint pointA = selectedPanel.GetComponent<WallPanel>().anchorStart;
                GraphGridPoint pointB = selectedPanel.GetComponent<WallPanel>().anchorEnd;
                windowInstance = CreateSection(pointA, pointB, windowPrefab, transform.parent);
                selectedPanel.transform.SetParent(tempHidden);
            }
        }
        _lastSelection = _selection;
    }
}

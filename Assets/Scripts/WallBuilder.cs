using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder : PointerSelector
{
    [SerializeField] string expectedTag = "FloorSnapPoint";
    bool isBuilding = false;
    [SerializeField] GameObject wallStartPilePrefab;
    [SerializeField] GameObject wallPanelPrefab;
    WallPanelScaler wallPanelScaler;
    [SerializeField] TilesGridController tilesGridController;
    [SerializeField] Transform wallsContainer;
    GameObject pileInstance;
    GameObject createdWall;
    Transform lastSelection;

    // Start is called before the first frame update
    void Start()
    {
        tilesGridController = GameObject.Find("TilesGridController")
            .GetComponent<TilesGridController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_selection != null)
        {
            if (_selection.gameObject.CompareTag(expectedTag)
                && _selection.gameObject.GetComponent<SnapPoint>().type == SnapPointType.WallGrid)
            {
                if (!isBuilding)
                {
                    if (pileInstance == null)
                        pileInstance = Instantiate(wallStartPilePrefab, _selection.position, wallStartPilePrefab.transform.rotation);
                    else
                        pileInstance.transform.position = _selection.position;
                }
                else
                {
                    if (lastSelection != null
                        && (lastSelection.position - _selection.position).magnitude >= tilesGridController.tileSize
                        && (lastSelection.position - _selection.position).magnitude < 3* tilesGridController.tileSize)
                    {
                        if (createdWall != null && createdWall.transform.position == _selection.position) return; // TODO
                        // need to implement validation if wall already exists between two points!
                        float rotationAngle = Vector3.SignedAngle(Vector3.right, _selection.position - lastSelection.position, Vector3.up);
                        Vector3 spawnPoint = lastSelection.transform.position;
                        float scale = (_selection.position - lastSelection.transform.position).magnitude;
                        //==========================================================================================================================
                        BuildSection(spawnPoint,rotationAngle,scale);
                        //==========================================================================================================================

                        pileInstance.transform.position = _selection.position;
                        lastSelection = _selection;
                    }                    
                }
            }
            else
            {
                _selection = null;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(pileInstance != null)
            {
                isBuilding = true;
                lastSelection = _selection;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(isBuilding)
            {
                isBuilding = false;
                Destroy(pileInstance);
            }
        }
    }

    public GameObject BuildSection(Vector3 _spawnPosition, float _rotationAngle, float _scale)
    {
        GameObject newSection = Instantiate(wallPanelPrefab, _spawnPosition, Quaternion.identity);

        newSection.transform.localEulerAngles =
            new Vector3(newSection.transform.eulerAngles.x, _rotationAngle, newSection.transform.eulerAngles.z);
        wallPanelScaler = newSection.GetComponent<WallPanelScaler>();
        wallPanelScaler.ScaleX(_scale);
        wallPanelScaler.endingTip.transform.localPosition = new Vector3(_scale, 0, 0);
        wallPanelScaler.transform.SetParent(wallsContainer);

        WallPanel wallPanel = newSection.GetComponent<WallPanel>();
        wallPanel.SetParameters(_spawnPosition, _rotationAngle, _scale);
        return newSection;
    }
}

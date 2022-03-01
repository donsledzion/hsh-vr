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

    GameObject pileInstance;
    GameObject createdWall;
    Transform lastSelection;

    // Start is called before the first frame update
    void Start()
    {
        tilesGridController = GameObject.Find("TilesGridController").GetComponent<TilesGridController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_selection != null)
        {
            if (_selection.gameObject.CompareTag(expectedTag) && _selection.gameObject.GetComponent<SnapPoint>().type == SnapPointType.WallGrid)
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
                    if (lastSelection != null)
                    {
                        float rotationAngle = Vector3.SignedAngle(Vector3.right, _selection.position - lastSelection.position, Vector3.up);

                        createdWall = Instantiate(wallPanelPrefab, pileInstance.transform.position, Quaternion.identity);
                        
                        createdWall.transform.localEulerAngles =
                            new Vector3(createdWall.transform.eulerAngles.x, rotationAngle, createdWall.transform.eulerAngles.z);
                        wallPanelScaler = createdWall.GetComponent<WallPanelScaler>();
                        wallPanelScaler.ScaleX((_selection.position - lastSelection.transform.position).magnitude);
                        wallPanelScaler.endingTip.transform.localPosition = new Vector3((_selection.position - pileInstance.transform.position).magnitude, 0, 0);

                        pileInstance.transform.position = _selection.position;
                        lastSelection = _selection;
                    }
                    
                }
            }
            else
            {
                Destroy(_selection);
            }
        }

        if(isBuilding)
        {

        }

        if(Input.GetMouseButtonDown(0))
        {
            if(pileInstance != null)
            {
                isBuilding = true;
                lastSelection = _selection;

                //Destroy(pileInstance);
                //Quaternion directionAngle = Quaternion.FromToRotation(pileInstance.transform.position,Input.mousePosition);
                /*createdWall = Instantiate(wallPanelPrefab, pileInstance.transform.position, Quaternion.identity);
                wallPanelScaler = createdWall.GetComponent<WallPanelScaler>();*/
            }
        }


        if(Input.GetMouseButtonUp(0))
        {
            if(isBuilding)
            {
                isBuilding = false;
            }
        }
    }
}

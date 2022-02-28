using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBaseSelector : PointerSelector
{
    [SerializeField] string expectedTag = "FloorSnapPoint";
    bool isBuilding = false;
    [SerializeField] GameObject wallStartPilePrefab;
    [SerializeField] GameObject wallPanelPrefab;
    WallPanelScaler wallPanelScaler;
    TilesGridController tilesGridController;

    GameObject pileInstance;
    GameObject createdWall;

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
            if (_selection.gameObject.CompareTag(expectedTag))
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
                    float rotationAngle = Vector3.SignedAngle(Vector3.right,_selection.position-createdWall.transform.position,Vector3.up);
                    createdWall.transform.localEulerAngles = 
                        new Vector3(createdWall.transform.eulerAngles.x, rotationAngle, createdWall.transform.eulerAngles.z);
                    wallPanelScaler.ScaleX((_selection.position - createdWall.transform.position).magnitude/0.25f);
                    wallPanelScaler.endingTip.transform.localPosition = new Vector3((_selection.position - createdWall.transform.position).magnitude,0,0);
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
                //Destroy(pileInstance);
                //Quaternion directionAngle = Quaternion.FromToRotation(pileInstance.transform.position,Input.mousePosition);
                createdWall = Instantiate(wallPanelPrefab, pileInstance.transform.position, Quaternion.identity);
                wallPanelScaler = createdWall.GetComponent<WallPanelScaler>();
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

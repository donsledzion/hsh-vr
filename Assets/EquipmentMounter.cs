using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentMounter : MonoBehaviour
{
    [SerializeField] public GameObject equipmentPrefab;
    [SerializeField] GameObject equipmentInstance;
    protected Transform _mountPoint;
    [SerializeField] LayerMask layerMask;

    private void Update()
    {
        
        if (equipmentPrefab != null)
        {
            layerMask = equipmentPrefab.GetComponent<Equipment>().layerMask;

            CheckMountPoint();

            if (equipmentInstance == null)
                equipmentInstance = Instantiate(equipmentPrefab, _mountPoint.position, equipmentPrefab.transform.rotation);
            else
            {
                equipmentInstance.transform.position = _mountPoint.position;
                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Setting equipment");
                    equipmentPrefab = null;
                    equipmentInstance = null;
                }
            }
                


            if (Input.GetKeyDown(KeyCode.R))
                equipmentInstance.GetComponent<Equipment>().Rotate(45f);
        }
    }

    private void CheckMountPoint()
    {
        if (_mountPoint != null)
        {
            _mountPoint = null;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            _mountPoint = equipmentPrefab.transform;
            _mountPoint.position = hit.point;
        }
    }

}

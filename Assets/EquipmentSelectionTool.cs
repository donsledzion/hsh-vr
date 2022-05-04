using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelectionTool : PointerSelector
{
    [SerializeField] GameObject selectionPointerPrefab;
    GameObject selectionPointerInstance;
    GameObject pickedObject;

    protected override void Update()
    {
        base.Update();

        if (_selection != null)
        {
            Debug.Log(_selection.gameObject.name);

            if (Input.GetMouseButtonDown(0))
                pickedObject = _selection.gameObject;            
        }

        if (pickedObject != null)
        {
            if (selectionPointerInstance == null)
                selectionPointerInstance = Instantiate(selectionPointerPrefab, pickedObject.transform);
            selectionPointerInstance.transform.position = pickedObject.transform.position + Vector3.up * pickedObject.GetComponent<BoxCollider>().size.y * 1.2f;

            if (Input.GetMouseButtonDown(1))
            {
                pickedObject = null;
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Destroy(pickedObject);
                pickedObject = null;
            }
        }
        else
        {
            Destroy(selectionPointerInstance);
            selectionPointerInstance = null;
        }
    }
    
}

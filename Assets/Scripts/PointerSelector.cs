using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSelector : MonoBehaviour
{
    [SerializeField] float scaleFactor = 10f;

    private Vector3 originalScale;
    protected Transform _selection;
    [SerializeField] LayerMask layerMask;

    protected virtual void Update()
    {
        if(_selection != null)
        {
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,Mathf.Infinity,layerMask))
        {
            _selection = hit.transform;
        }
    }
}

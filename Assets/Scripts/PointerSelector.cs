using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSelector : MonoBehaviour
{
    [SerializeField] float scaleFactor = 10f;

    private Vector3 originalScale;
    protected Transform _selection;
    [SerializeField] LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(_selection != null)
        {
            //_selection.localScale = originalScale;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,Mathf.Infinity,layerMask))
        {
            _selection = hit.transform;/*
            originalScale = _selection.localScale;
            _selection.localScale *= scaleFactor;*/
        }
    }
}

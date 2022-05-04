using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSelector : MonoBehaviour
{
    protected Transform _selection;
    [SerializeField] LayerMask layerMask;
    protected MainManager mainManager;

    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    protected virtual void Update()
    {
        if(_selection != null)
        {
            _selection = null;
        }
        //var ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,Mathf.Infinity,layerMask) && !mainManager.cursorOverUI)
        {
            _selection = hit.transform;
        }   
    }
}



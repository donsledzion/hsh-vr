using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPanelScaler : MonoBehaviour
{
    [SerializeField] GameObject scalableObject;
    [SerializeField] public GameObject endingTip;
    // Start is called before the first frame update
    
    public void ScaleX(float scale)
    {
        scalableObject.transform.localScale = new Vector3(scale,1f,1f);
    }
}

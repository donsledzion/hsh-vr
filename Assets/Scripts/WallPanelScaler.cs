using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPanelScaler : MonoBehaviour
{
    [SerializeField] protected GameObject scalableObject;
    [SerializeField] protected GameObject endingTip;
    
    public virtual void ScaleX(float scale)
    {
        scalableObject.transform.localScale = new Vector3(scale,1f,1f);
        endingTip.transform.Translate(Vector3.right * scale);
    }
}

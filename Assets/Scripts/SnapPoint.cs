using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public SnapPointType type = SnapPointType.Default;

 
    public void SetType(SnapPointType _type)
    {
        type = _type;
        if (_type == SnapPointType.WallGrid)
            GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
public enum SnapPointType
{
    Default,
    WallGrid
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPanelScaler : WallPanelScaler
{
    [SerializeField] public GameObject doorSocket;

    public override void ScaleX(float scale)
    {
        base.ScaleX(scale);

    }
}

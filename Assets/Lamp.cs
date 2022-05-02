using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Equipment
{
    [SerializeField] List<Light> lights;



    public void SetLights(bool isOn)
    {
        foreach (Light light in lights)
            light.enabled = isOn;
    }
}

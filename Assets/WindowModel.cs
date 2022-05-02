using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowModel : MonoBehaviour
{
    [SerializeField] GameObject modelSection;


    public void TurnAround()
    {
        modelSection.transform.Rotate(Vector3.up, 180);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] public LayerMask layerMask;

    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.up,angle);
    }

    public enum EquipmentSnapMode
    {
        Floor,
        Wall,
        Ceiling
    }
}

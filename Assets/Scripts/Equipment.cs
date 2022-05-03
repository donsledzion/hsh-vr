using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] public LayerMask layerMask;
    [SerializeField] protected RoomTag roomTag;
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

    public enum RoomTag
    {
        Kitchen,
        Bathroom,
        Bedroom,
        Dinning,
        Corridor,
        Toilet,
        LivingRoom
    }
}

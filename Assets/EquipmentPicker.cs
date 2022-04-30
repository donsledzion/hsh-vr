using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPicker : MonoBehaviour
{
    [SerializeField] GameObject equipmentPrefab;
    [SerializeField] EquipmentMounter equipmentMounter;


    public void MountEquipment()
    {
        equipmentMounter.equipmentPrefab = equipmentPrefab;
    }
}

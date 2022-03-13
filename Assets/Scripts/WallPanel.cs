using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class WallPanel : MonoBehaviour
{
    [Serializable]
    public struct WallCreationData
    {
        public Vector3 spawnPosition;
        public float rotationAngle;
        public float scale;
    }

    [SerializeField]
    public WallCreationData creationData;
    
    public void SetParameters(Vector3 _spawnPosition, float _rotationAngle, float _scale)
    {
        creationData.spawnPosition = _spawnPosition;
        creationData.rotationAngle = _rotationAngle;
        creationData.scale = _scale;
    }

    public WallCreationData GetParameters()
    {
        return creationData;
    }
}

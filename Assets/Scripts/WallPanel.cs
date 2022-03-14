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

    public GraphGridPoint anchorStart, anchorEnd;
    [SerializeField] Material originalMaterial, deletingMaterial;
    [SerializeField] Renderer[] renderers;

    
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

    public bool MatchAnchors(GraphGridPoint p1, GraphGridPoint p2)
    {
        if((anchorStart == p1 && anchorEnd == p2)
            || (anchorStart == p2 && anchorEnd == p1))
                return true;
        return false;
    }
}

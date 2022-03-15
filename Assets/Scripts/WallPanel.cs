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

    public bool MatchPath(List<GraphGridPoint> path)
    {
        foreach(GraphGridPoint point in path)
        {
            if (anchorStart == point && path.Contains(anchorEnd))
                return true;
            if (anchorEnd == point && path.Contains(anchorStart))
                return true;
        }

        return false;
    }

    public void PaintOriginal()
    {
        foreach (Renderer renderer in renderers)
            renderer.material = originalMaterial;
    }

    public void PaintDeleted()
    {
        foreach (Renderer renderer in renderers)
        {
            Debug.Log("Deleting material...");
            renderer.material = deletingMaterial;
        }
            
    }
}
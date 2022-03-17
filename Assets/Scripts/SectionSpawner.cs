using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionSpawner : PointerSelector
{
    protected GameObject CreateSection(GraphGridPoint pointA, GraphGridPoint pointB, GameObject sectionPrefab, Transform destination)
    {
        Vector3 progresVector = (pointB.transform.position - pointA.transform.position);
        float scale = progresVector.magnitude;
        Vector3 spawnPoint = pointA.transform.position;
        float rotation = Vector3.SignedAngle(Vector3.right, progresVector, Vector3.up);

        GameObject newSection = Instantiate(sectionPrefab, spawnPoint, Quaternion.identity);

        newSection.GetComponent<WallPanel>().anchorStart = pointA;
        newSection.GetComponent<WallPanel>().anchorEnd = pointB;

        newSection.transform.localEulerAngles = newSection.transform.localEulerAngles =
            new Vector3(newSection.transform.eulerAngles.x, rotation, newSection.transform.eulerAngles.z);
        WallPanelScaler wallPanelScaler = newSection.GetComponent<WallPanelScaler>();
        wallPanelScaler.ScaleX(scale);
        newSection.transform.SetParent(destination);

        return newSection;
    }
}

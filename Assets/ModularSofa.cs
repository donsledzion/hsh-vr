using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularSofa : MonoBehaviour
{
    Vector3 sofaSpanwPoint = Vector3.zero;

    [SerializeField] GameObject leftSectionPrefab;
    [SerializeField] GameObject rightSectionPrefab;
    [SerializeField] GameObject middleSectionPrefab;

    [SerializeField] Vector3 leftSectionDimensions;
    [SerializeField] Vector3 rightSectionDimensions;
    [SerializeField] Vector3 middleSectionDimensions;

    [SerializeField] Vector3 leftSectionSpawnPoint;
    [SerializeField] Vector3 rightSectionSpawnPoint;
    [SerializeField] Vector3 middleSectionSpawnPoint;

    GameObject leftSectionInstance;
    GameObject rightSectionInstance;
    GameObject middleSectionInstance;

    [SerializeField] bool threeSections = false;

    void Start()
    {
        leftSectionInstance = Instantiate(leftSectionPrefab, leftSectionSpawnPoint, Quaternion.identity);
        rightSectionInstance = Instantiate(rightSectionPrefab, rightSectionSpawnPoint, Quaternion.identity);
        middleSectionInstance = Instantiate(middleSectionPrefab, middleSectionSpawnPoint, Quaternion.identity);
        middleSectionInstance.SetActive(false);

        leftSectionDimensions = leftSectionInstance.GetComponent<BoxCollider>().size;
        rightSectionDimensions = rightSectionInstance.GetComponent<BoxCollider>().size;
        middleSectionDimensions = middleSectionInstance.GetComponent<BoxCollider>().size;

        if (threeSections)
            SetThreeSections();
        else
            SetTwoSections();

    }

    void Update()
    {
        
    }

    public void SetTwoSections()
    {
        middleSectionInstance.SetActive(false);
        leftSectionInstance.transform.position = sofaSpanwPoint + new Vector3(leftSectionDimensions.x / 2, 0, 0);
        rightSectionInstance.transform.position = sofaSpanwPoint - new Vector3(rightSectionDimensions.x / 2, 0, 0);
    }

    public void SetThreeSections()
    {
        leftSectionInstance.transform.position = sofaSpanwPoint + new Vector3(middleSectionDimensions.x/ 2 + leftSectionDimensions.x / 2, 0, 0);
        middleSectionInstance.transform.position = sofaSpanwPoint;
        middleSectionInstance.SetActive(true);
        rightSectionInstance.transform.position = sofaSpanwPoint - new Vector3(middleSectionDimensions.x/ 2 + rightSectionDimensions.x / 2, 0, 0);
    }
}

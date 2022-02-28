using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] Vector3 centerPoint = Vector3.zero;
    [SerializeField] float rotationStep = 45f;
    [SerializeField] float smoothRotation = 1f;
    [SerializeField] float currentRotation = 0f;

    [SerializeField] bool turnOffSimpleCameraController = true;
    // Start is called before the first frame update
    void Start()
    {
        if (turnOffSimpleCameraController)
            gameObject.GetComponent<SimpleCameraController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            RotateLeft();
        if (Input.GetKeyDown(KeyCode.E))
            RotateRight();
    }

    void RotateLeft()
    {
        Rotate(-rotationStep);
    }

    void RotateRight()
    {
        Rotate(rotationStep);
    }

    void Rotate(float angle=1f)
    {
        transform.RotateAround(centerPoint, Vector3.up, angle);
    }
}

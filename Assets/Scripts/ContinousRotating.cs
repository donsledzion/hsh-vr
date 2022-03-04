using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousRotating : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 rotationSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}

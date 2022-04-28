using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSwitcher : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject vrPlayer;
    [SerializeField] GameObject builders;
    [SerializeField] GameObject grid;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        builders.SetActive(mainCamera.activeSelf);
        grid.SetActive(mainCamera.activeSelf);
        vrPlayer.SetActive(!vrPlayer.activeSelf);
    }
}

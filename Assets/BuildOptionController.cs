using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildOptionController : MonoBehaviour
{
    [SerializeField] GameObject wallsBuilder;

    [SerializeField] GameObject singleDoorsBuilder;
    [SerializeField] GameObject doorsModelPicker;

    [SerializeField] GameObject singleWindowsBuilder;

    public void BuildWalls()
    {
        SelectOption(BuildOption.BuildWalls);
    }
    public void BuildSingleDoors()
    {
        SelectOption(BuildOption.BuildSingleDoors);
    }
    public void BuildSingleWindows()
    {
        SelectOption(BuildOption.BuildSingleWindows);
    }

    void SelectOption(BuildOption option)
    {
        switch(option)
        {
            case BuildOption.BuildWalls:
            {
                    ShutDownAll();
                    wallsBuilder.gameObject.SetActive(true);                    
                    break;
            }
            case BuildOption.BuildSingleDoors:
            {
                    ShutDownAll();
                    singleDoorsBuilder.gameObject.SetActive(true);
                    doorsModelPicker.gameObject.SetActive(true);
                    break;
            }
            case BuildOption.BuildSingleWindows:
            {
                    ShutDownAll();
                    singleWindowsBuilder.gameObject.SetActive(true);                    
                    break;
            }
                default:
                    break;
        }
    }

    void ShutDownAll()
    {
        wallsBuilder.SetActive(false);
        singleDoorsBuilder.SetActive(false);
        doorsModelPicker.SetActive(false);
        singleWindowsBuilder.SetActive(false);
    }
}

public enum BuildOption
{
    BuildWalls,
    BuildSingleDoors,
    BuildDoubleDoors,
    BuildSingleWindows,
    BuilDoubleWindows,
    BuildTripleWindows
}

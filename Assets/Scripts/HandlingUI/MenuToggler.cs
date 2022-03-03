using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToggler : MonoBehaviour
{
    [SerializeField] GameObject menuToToggle;

    public void Toggle()
    {
        if(menuToToggle!=null)
            menuToToggle.SetActive(!menuToToggle.activeSelf);
    }
}

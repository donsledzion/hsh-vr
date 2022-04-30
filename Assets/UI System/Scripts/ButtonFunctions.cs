using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject ExitModalWindow;
    public GameObject MainMenuModalWindow;
    public GameObject SettingsModalWindow;
    public GameObject SlideDownMenu;

    public void ShowExitModalWindow(){
        ExitModalWindow.SetActive(true);
        MainMenuModalWindow.SetActive(false);
        SettingsModalWindow.SetActive(false);
    }

    public void HideExitModalWindow(){
        ExitModalWindow.SetActive(false);
    }

    public void ShowMainMenuModalWindow(){
        MainMenuModalWindow.SetActive(true);
        ExitModalWindow.SetActive(false);
        SettingsModalWindow.SetActive(false);
    }

    public void HideMainMenuModalWindow(){
        MainMenuModalWindow.SetActive(false);
    }

    public void ShowSettingsModalWindow(){

        SettingsModalWindow.SetActive(true);
        MainMenuModalWindow.SetActive(false);
        ExitModalWindow.SetActive(false);
    }

    public void HideSettingsModalWindow(){
        SettingsModalWindow.SetActive(false);
    }

    public void HideAllModalWindow(){

        SettingsModalWindow.SetActive(false);
        MainMenuModalWindow.SetActive(false);
        ExitModalWindow.SetActive(false);
    }

    public void ExitApp(){

        Application.Quit();
        Debug.Log("Quit");
        UnityEditor.EditorApplication.isPlaying = false;
    }
}

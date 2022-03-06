using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : SlotButton
{
    [SerializeField] MainManager mainManager;
    [SerializeField] GameObject savedInfo;
    [SerializeField] GameObject inputName;

    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    public void SaveSlot(string slotName)
    {
        mainManager.SaveData(slotName);
    }

    public void ShowInput()
    {
        savedInfo.gameObject.SetActive(false);
        inputName.gameObject.SetActive(true);
    }

    public void ShowInfo()
    {
        savedInfo.gameObject.SetActive(true);
        inputName.gameObject.SetActive(false);
    }


}

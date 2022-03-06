using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    SimpleCameraController cameraController;
    SaveButton saveButton;
    [SerializeField] TextMeshProUGUI inputField;
    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<SimpleCameraController>();
        saveButton = gameObject.GetComponentInParent<SaveButton>();
        
    }

    public void CameraAndControllsActive(bool isEnabled)
    {
        cameraController.enabled = isEnabled;
    }

    public void UpdateButtonName()
    {
        saveButton.UpdateButton(inputField.text, Vector2.zero, "today");
    }

    public void SaveSlot()
    {
        UpdateButtonName();
        saveButton.SaveSlot(saveButton.slotNameTMP.text);
    }
}

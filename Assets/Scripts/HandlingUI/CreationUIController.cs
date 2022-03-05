using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class CreationUIController : MonoBehaviour
{
    // Start is called before the first frame update
    RawImage bgImage;
    [SerializeField] GameObject selectNewSize;
    [SerializeField] TilesGridController gridController;
    [SerializeField] GameObject inputSizeX;
    [SerializeField] GameObject inputSizeY;

    [Space]
    [SerializeField] GameObject newOrLoadButtons;
    [SerializeField] MainManager mainManager;
    [Space]
    [SerializeField] UnityEvent onSessionLoad;

    void Start()
    {
        bgImage = GetComponent<RawImage>();
    }

    public void SelectGridSize()
    {
        newOrLoadButtons.SetActive(false);
        selectNewSize.SetActive(true);
    }

    public void StartNewCreation()
    {
        float sizeX=0f, sizeY=0f;

        float.TryParse(inputSizeX.GetComponent<TMP_InputField>().text, out sizeX);
        float.TryParse(inputSizeY.GetComponent<TMP_InputField>().text, out sizeY);

        if (sizeX == 0f) sizeX = 10f;
        if (sizeY == 0f) sizeY = 10f;

        gridController.GenerateGrid(sizeX, sizeY);
        bgImage.enabled = false;
        selectNewSize.SetActive(false);
    }

    public void LoadSession()
    {
        onSessionLoad?.Invoke();
    }

}

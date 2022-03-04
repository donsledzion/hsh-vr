using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        float sizeX, sizeY;

        float.TryParse(inputSizeX.GetComponent<TMP_InputField>().text, out sizeX);
        float.TryParse(inputSizeY.GetComponent<TMP_InputField>().text, out sizeY);

        gridController.GenerateGrid(sizeX, sizeY);
        bgImage.enabled = false;
        selectNewSize.SetActive(false);
    }

    public void LoadGame()
    {
        bgImage.enabled = false;
        newOrLoadButtons.SetActive(false);
    }

}

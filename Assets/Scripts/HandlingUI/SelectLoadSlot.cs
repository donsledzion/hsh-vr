using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectLoadSlot : MonoBehaviour
{
    [SerializeField] GameObject loadButtonPrefab;
    [SerializeField] GameObject goBackButtonPrefab;
    [SerializeField] MainManager mainManager;
    [SerializeField] CreationUIController uiController;
    [SerializeField] int maxSlotsCount = 5;
    [SerializeField] float buttonsOffset = 5;
    [Space]
    [SerializeField] UnityEvent onBackButton;

    public void DrawButtons()
    {
        ClearButtons();
        string[] slots = Directory.GetDirectories(Application.persistentDataPath, "*",SearchOption.TopDirectoryOnly);
        float loadButtonHeight = loadButtonPrefab.GetComponent<RectTransform>().rect.height;
        for (int i = 0; i < maxSlotsCount; i++)
        {
            if(i < slots.Length)
            {
                string slotName = Path.GetFileName(slots[i]);
                
                GameObject loadButton = Instantiate(
                    loadButtonPrefab,
                    transform.position + new Vector3(0, -i * (loadButtonHeight+buttonsOffset), 0),
                    transform.rotation);

                loadButton.transform.SetParent(transform);
                LoadButton loadButtonController = loadButton.GetComponent<LoadButton>();
                loadButtonController.UpdateButton(slotName, new Vector2(10, 10), "jakas data"); // read grid size and sessio date!!! TODO!!!
                loadButton.GetComponent<Button>().onClick.AddListener(() => mainManager.LoadSavedData(slotName));
                loadButton.GetComponent<Button>().onClick.AddListener(() => uiController.LoadSession());
            }
            else
            {
                string slotName = "< PUSTY >";
                GameObject loadButton = Instantiate(
                    loadButtonPrefab,
                    transform.position + new Vector3(0, -i * (loadButtonHeight + buttonsOffset), 0),
                    transform.rotation);

                loadButton.transform.SetParent(transform);
                LoadButton loadButtonController = loadButton.GetComponent<LoadButton>();
                loadButtonController.UpdateButton(slotName, new Vector2(0, 0), "n/d");
            }
        }

        GameObject goBackButton = Instantiate(
                goBackButtonPrefab,
                transform.position + new Vector3(0, -maxSlotsCount * (loadButtonHeight + buttonsOffset), 0),
                transform.rotation);
        goBackButton.transform.SetParent(transform);
        goBackButton.GetComponent<Button>().onClick.AddListener(() => onBackButton.Invoke());
    }

    void ClearButtons()
    {
        Transform[] children = gameObject.transform.GetComponentsInChildren<Transform>();
        foreach(Transform child in children)
        {
            if(child.gameObject!=transform.gameObject)
                Destroy(child.gameObject);
        }
    }
}

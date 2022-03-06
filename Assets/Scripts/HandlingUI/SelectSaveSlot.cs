using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectSaveSlot : ButtonSlot
{
    public void DrawButtons()
    {
        ClearButtons();
        string[] slots = Directory.GetDirectories(Application.persistentDataPath, "*", SearchOption.TopDirectoryOnly);
        float loadButtonHeight = slotButtonPrefab.GetComponent<RectTransform>().rect.height;
        for (int i = 0; i < maxSlotsCount; i++)
        {
            if (i < slots.Length)
            {
                string slotName = Path.GetFileName(slots[i]);

                GameObject button = Instantiate(
                    slotButtonPrefab,
                    transform.position + new Vector3(0, -i * (loadButtonHeight + buttonsOffset), 0),
                    transform.rotation);

                button.transform.SetParent(transform);
                SlotButton loadButtonController = button.GetComponent<SlotButton>();
                loadButtonController.UpdateButton(slotName, new Vector2(10, 10), "jakas data"); // read grid size and sessio date!!! TODO!!!
                button.GetComponent<Button>().onClick.AddListener(() => mainManager.SaveData(slotName));
            }
            else
            {
                string slotName = "< PUSTY >";
                GameObject button = Instantiate(
                    slotButtonPrefab,
                    transform.position + new Vector3(0, -i * (loadButtonHeight + buttonsOffset), 0),
                    transform.rotation);

                button.transform.SetParent(transform);
                SaveButton saveButtonController = button.GetComponent<SaveButton>();
                saveButtonController.UpdateButton(slotName, new Vector2(0, 0), "n/d");
                //saveButtonController.SetEmpty();
                button.GetComponent<Button>().onClick.AddListener(() => saveButtonController.ShowInput());
            }
        }

        GameObject goBackButton = Instantiate(
                goBackButtonPrefab,
                transform.position + new Vector3(0, -maxSlotsCount * (loadButtonHeight + buttonsOffset), 0),
                transform.rotation);
        goBackButton.transform.SetParent(transform);
        goBackButton.GetComponent<Button>().onClick.AddListener(() => onBackButton.Invoke());
    }
}

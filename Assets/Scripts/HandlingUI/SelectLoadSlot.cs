using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectLoadSlot : ButtonSlot
{

    public void DrawButtons()
    {
        ClearButtons();
        string[] slots = Directory.GetDirectories(Application.persistentDataPath, "*",SearchOption.TopDirectoryOnly);
        float loadButtonHeight = slotButtonPrefab.GetComponent<RectTransform>().rect.height;
        for (int i = 0; i < maxSlotsCount; i++)
        {
            if(i < slots.Length)
            {
                string slotName = Path.GetFileName(slots[i]);
                
                GameObject loadButton = Instantiate(
                    slotButtonPrefab,
                    transform.position + new Vector3(0, -i * (loadButtonHeight+buttonsOffset), 0),
                    transform.rotation);

                loadButton.transform.SetParent(transform);
                SlotButton loadButtonController = loadButton.GetComponent<SlotButton>();
                loadButtonController.UpdateButton(slotName, new Vector2(10, 10), "jakas data"); // read grid size and sessio date!!! TODO!!!
                loadButton.GetComponent<Button>().onClick.AddListener(() => mainManager.LoadSavedData(slotName));
                loadButton.GetComponent<Button>().onClick.AddListener(() => uiController.LoadSession());
            }
            else
            {
                string slotName = "< PUSTY >";
                GameObject loadButton = Instantiate(
                    slotButtonPrefab,
                    transform.position + new Vector3(0, -i * (loadButtonHeight + buttonsOffset), 0),
                    transform.rotation);

                loadButton.transform.SetParent(transform);
                SlotButton loadButtonController = loadButton.GetComponent<SlotButton>();
                loadButtonController.UpdateButton(slotName, new Vector2(0, 0), "n/d");
            }
        }

        Vector2 backButtonPos = new Vector2(0, -maxSlotsCount * (loadButtonHeight + buttonsOffset));

        AddBackButton(backButtonPos);
    }

    
}
